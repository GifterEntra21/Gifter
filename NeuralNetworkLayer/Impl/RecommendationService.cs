using DataAccessLayer.Interfaces;
using Entities;
using NeuralNetworkLayer.Interfaces;
using Shared;
using Shared.Responses;

namespace NeuralNetworkLayer.Impl
{
    public class RecommendationService:IRecommendationModel
    {
        public readonly IProductDAL _ProductService;

        public RecommendationService(IProductDAL productService)
        {
            _ProductService = productService;
        }

        private InstagramProfile CategorizeProfileByTags(List<TagWithCount> tags, string userName)
        {
            InstagramProfile profile = new(userName);

            Dictionary<string, int> genresBiases = new Dictionary<string, int>();

            foreach (GenreWithTags genre in TagGenres.GenresList)
            {
                int genreTagCounts = 0;
                for (int i = 0; i < tags.Count; i++)
                {
                    if (genre.Tags.Contains(tags[i].Name))
                    {
                        genreTagCounts += tags[i].Count;
                    }
                }
                genresBiases.Add(genre.GenreName, genreTagCounts);
            }

            genresBiases = genresBiases.OrderByDescending(g => g.Value).ToDictionary(g => g.Key, g => g.Value);

            profile.Genre = genresBiases.First().Key;
            return profile;
        }

        public async Task<DataResponse<Product>> GetGifts(List<TagWithCount> tags, string username)
        {
            InstagramProfile profile = CategorizeProfileByTags(tags, username);

            return await _ProductService.GetByGenre(profile.Genre);
        }

    }
}
