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
        public readonly IProductBLL _productService;

        public WebScrapperBLL(IWebScrapperDAL webScrapperService, IRecommendationModel recommendationModelService, IDistributedCache cache, IProductBLL productService)
        {
            _webScrapperService = webScrapperService;
            _recommendationModelService = recommendationModelService;
            _cache = cache;
            _productService = productService;
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

        public async Task<DataResponse<Product>> VerifyProfile(string username)
        {
            try
            {
                Dictionary<string, string> profilesCache = new();
                //busca o cache do redis
                string json = await _cache.GetStringAsync("Profiles");
                //se nao for nulo deserializa
                if (!string.IsNullOrWhiteSpace(json))
                {

                    profilesCache = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    //se existir retorna os produtos
                    if (profilesCache.ContainsKey(username))
                    {
                        DataResponse<Product> products = await _productService.GetByGenre(profilesCache[username]);

                        return ResponseFactory.CreateInstance().CreateSuccessDataResponse<Product>(products.ItemList);
                    }
                }
                //como o perfil nao existe categoriza
                DataResponse<TagWithCount> tags = await Scrape(username);
                if (!tags.HasSuccess)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(null, "nao foi possivel verficar o perfil");
                }
                SingleResponse<InstagramProfile> profile = await _recommendationModelService.CategorizeProfileByTags(tags.ItemList, username);
                if (!profile.HasSucces)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(null, "nao foi possivel categorizar o perfil");
                }
                //adiciona na lista, nao importa se estiver vazia ou nao
                profilesCache.Add(username, profile.Item.Genre);
                //converte pra  json
                string newProfileCache = JsonSerializer.Serialize(profilesCache);
                //Setado novamente
                await _cache.SetStringAsync("Profiles", newProfileCache);

                DataResponse<Product> newProducts = await _productService.GetByGenre(profile.Item.Genre);
                if (!newProducts.HasSuccess)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(null, "nao foir possivel acessar os produtos");
                }

                return ResponseFactory.CreateInstance().CreateSuccessDataResponse<Product>(newProducts.ItemList);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Product>(ex);
            }
        }


    }
}
