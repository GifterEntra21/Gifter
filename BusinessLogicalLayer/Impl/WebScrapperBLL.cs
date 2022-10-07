﻿using BusinessLogicalLayer.Interfaces;
using DataAccessLayer.Impl;
using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using NeuralNetworkLayer;
using NeuralNetworkLayer.Interfaces;
using Shared.Responses;

namespace BusinessLogicalLayer.Impl
{
    public class WebScrapperBLL : IWebScrapperBLL
    {
        public readonly IWebScrapperDAL _webScrapperService;
        public readonly IRecommendationModel _recommendationModelService;

        public WebScrapperBLL(IWebScrapperDAL webScrapperService, IRecommendationModel recommendationModelService)
        {
            _webScrapperService = webScrapperService;
            _recommendationModelService = recommendationModelService;
        }

        public async Task<List<TagWithCount>> Scrape(string profile)
        {
            ComputerVision vision = new ComputerVision();
            var scrape = await _webScrapperService.ScrapeInstagramWithDefaultAccount(false, profile);
            List<ImageTag> tags = await vision.CheckTags(scrape);
            List<string> tagsNames = new List<string>();
            foreach (var tag in tags)
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

            return tagsWithCount;
        }

        public async Task<DataResponse<Product>> GetGifts(List<TagWithCount> tags, string username)
        {
            return await _recommendationModelService.GetGifts(tags, username);
        }
    }
}