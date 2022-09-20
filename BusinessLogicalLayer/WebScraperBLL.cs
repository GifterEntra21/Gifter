using DataAccessLayer;
using Entities;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using NeuralNetworkLayer;

namespace BusinessLogicalLayer
{
    public static class WebScraperBLL
    {
        public static async Task<List<TagWithCount>> Scrape(string profile)
        {
            ComputerVision vision = new ComputerVision();
            var scrape = WebScraperDAL.ScrapeInstagramWithDefaultAccount(false, profile);
            List<ImageTag> tags = await vision.CheckTags(scrape);
            List<string> tagsNames = new List<string>();
            foreach (var tag in tags)
            {
                tagsNames.Add(tag.Name);
            }

            // Verifica as tags da lista e remove da lista aquelas que estão na blacklist
            for (int i = 0; i < NegativeParameters.NegativeList.Count; i++)
            {
                int indexToRemove = tagsNames.IndexOf(NegativeParameters.NegativeList[i]);
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

        public static async Task<List<Product>> GetGifts(List<TagWithCount> tags, string username)
        {
            return await RecommendationModel.GetGifts(tags, username);
        }
    }
}