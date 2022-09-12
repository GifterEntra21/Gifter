using DataAccessLayer;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using NeuralNetworkLayer;

namespace BusinessLogicalLayer
{
    public static class WebScraperBLL
    {
        public static async Task<List<string>> Scrape()
        {
            ComputerVision vision = new ComputerVision();
            var scrape = WebScraperDAL.ScrapeInstagramWithDefaultAccount(false, "neymarjr");
            List<ImageTag> tags = await vision.CheckTags(scrape);
            List<string> tagsNames = new List<string>();
            foreach (var tag in tags)
            {
                tagsNames.Add(tag.Name);
            }

            return tagsNames;
        }
    }
}