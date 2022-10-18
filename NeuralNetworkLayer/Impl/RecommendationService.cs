using DataAccessLayer.Interfaces;
using Entities;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using NeuralNetworkLayer.Interfaces;
using Shared;
using Shared.Responses;

namespace NeuralNetworkLayer.Impl
{
    public class RecommendationService : IRecommendationModel
    {
        public readonly IProductDAL _ProductService;

        public RecommendationService(IProductDAL productService)
        {
            _ProductService = productService;
        }

        private InstagramProfile CategorizeProfileByTags(List<TagWithCount> tags, string userName)
        {
            InstagramProfile profile = new(userName);

            Dictionary<string, int> genresTagsCounts = new Dictionary<string, int>();
            bool isGeneric = true;

            foreach (TagWithCount tag in tags)
            {
                foreach (GenreWithTags genre in TagGenres.GenresList)
                {
                    if (genre.Tags.Contains(tag.Name))
                    {
                        isGeneric = false;
                        int totalCount = genresTagsCounts.GetValueOrDefault(genre.GenreName) + tag.Count;
                        if (genresTagsCounts.ContainsKey(genre.GenreName))
                        {
                            genresTagsCounts[genre.GenreName] = totalCount;
                        }
                        else
                        {
                            genresTagsCounts.Add(genre.GenreName, totalCount);
                        }
                    }
                }
            }

            if (isGeneric)
            {
                profile.Genre = "generic";
                return profile;
            }

            genresTagsCounts = genresTagsCounts.OrderByDescending(g => g.Value).ToDictionary(g => g.Key, g => g.Value);

            profile.Genre = genresTagsCounts.First().Key;
            return profile;
        }

        public async Task<DataResponse<Product>> GetGifts(List<TagWithCount> tags, string username)
        {
            InstagramProfile profile = CategorizeProfileByTags(tags, username);

            return await _ProductService.GetByGenre(profile.Genre);
        }

    }
}
