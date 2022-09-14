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
            for (int i = 0; i < tags.Count; i++)
            {
                for (int j = 0; j < WordsBlacklist.Blacklist.Count; j++)
                {
                    if (tags[i].Name == WordsBlacklist.Blacklist[j] || tags[i].Confidence < 0.75)
                    {
                        tags.Remove(tags[i]);
                        i--;
                        break;
                    }
                }
            }

            foreach (var tag in tags)
            {
                tagsNames.Add(tag.Name);
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
    }
}