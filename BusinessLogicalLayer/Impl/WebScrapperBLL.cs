using BusinessLogicalLayer.Interfaces;
using DataAccessLayer.Impl;
using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Caching.Distributed;
using NeuralNetworkLayer;
using NeuralNetworkLayer.Interfaces;
using Shared.Responses;
using System.Text.Json;

namespace BusinessLogicalLayer.Impl
{
    public class WebScrapperBLL : IWebScrapperBLL
    {
        public readonly IWebScrapperDAL _webScrapperService;
        public readonly IRecommendationModel _recommendationModelService;
        public readonly IDistributedCache _cache;

        public WebScrapperBLL(IWebScrapperDAL webScrapperService, IRecommendationModel recommendationModelService, IDistributedCache cache)
        {
            _webScrapperService = webScrapperService;
            _recommendationModelService = recommendationModelService;
            _cache = cache;
        }

        public async Task<DataResponse<TagWithCount>> Scrape(string profile)
        {
            try
            {
                ComputerVision vision = new ComputerVision();
                DataResponse<string> scrape = await _webScrapperService.ScrapeInstagramWithDefaultAccount(false, profile);
                if (!scrape.HasSuccess)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<TagWithCount>(null);
                }
                DataResponse<ImageTag> tags = await vision.CheckTags(scrape.ItemList);
                if (!tags.HasSuccess)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<TagWithCount>(null);
                }
                List<string> tagsNames = new List<string>();
                foreach (var tag in tags.ItemList)
                {
                    tagsNames.Add(tag.Name);
                }

                // Verifica as tags da lista e remove da lista aquelas que estão na blacklist
                for (int i = 0; i < BanishedTags.BanishedTagsList.Count; i++)
                {
                    int indexToRemove = tagsNames.IndexOf(BanishedTags.BanishedTagsList[i]);
                    if (indexToRemove >= 0)
                    {
                        tagsNames.RemoveAt(indexToRemove);
                        i--;
                    }
                }

                List<TagWithCount> tagsWithCount = new List<TagWithCount>();

                var result = tagsNames.GroupBy(x => x)
                                .Where(g => g.Count() > 1)
                                .Select(x => new { Element = x.Key, Count = x.Count() })
                                .ToList();
                var result2 = result.OrderByDescending(a => a.Count).ToList();

                for (int i = 0; i < result2.Count(); i++)
                {
                    TagWithCount t = new TagWithCount()
                    {
                        Count = result2[i].Count,
                        Name = result2[i].Element
                    };
                    tagsWithCount.Add(t);
                }

                return ResponseFactory.CreateInstance().CreateSuccessDataResponse<TagWithCount>(tagsWithCount);
            }
            catch (Exception ex)
            {

                return ResponseFactory.CreateInstance().CreateFailedDataResponse<TagWithCount>(ex);
            }

        }

        public async Task<DataResponse<Product>> GetGifts(List<TagWithCount> tags, string username)
        {
            return await _recommendationModelService.GetGifts(tags, username);
        }

        public async Task<DataResponse<Product>> VerifyProfile(string profile)
        {
            try
            {
                Dictionary<string, List<Product>> profilesCache = new();
                //busca o cache do redis
                string json = await _cache.GetStringAsync("Profiles");
                //se nao for nulo deserializa
                if (!string.IsNullOrWhiteSpace(json))
                {

                    profilesCache = JsonSerializer.Deserialize<Dictionary<string, List<Product>>>(json);
                    //se existir retorna os produtos
                    if (profilesCache.ContainsKey(profile))
                    {
                        return ResponseFactory.CreateInstance().CreateSuccessDataResponse<Product>(profilesCache[profile]);
                    }
                }
                //se nao existir ou for nulo
                //executa o scrapping
                DataResponse<TagWithCount> tags = await Scrape(profile);

                //se ele nao tiver sucesso, ja retorna o erro
                if (!tags.HasSuccess)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(null);
                }
                //busca os presentes no banco
                DataResponse<Product> giftsResponse = await GetGifts(tags.ItemList, profile);

                //se nao tiver sucesso ja retorna o erro
                if (!giftsResponse.HasSuccess)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(null);
                }
                //adiciona na lista, nao importa se estiver vazia ou nao
                profilesCache.Add(profile, giftsResponse.ItemList);
                //converte pra  json
                string newProfileCache = JsonSerializer.Serialize(profilesCache);

                //Setado novamente
                await _cache.SetStringAsync("Profiles", newProfileCache);

                return ResponseFactory.CreateInstance().CreateSuccessDataResponse<Product>(giftsResponse.ItemList);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(ex);
            }

        }
    }
}