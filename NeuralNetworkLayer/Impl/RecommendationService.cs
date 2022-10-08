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

            int sportBias = 0;
            int animeBias = 0;
            int exotericBias = 0;

            for (int i = 0; i < tags.Count; i++)
            {
                if (TagGenres.SportTags.Contains(tags[i].Name))
                {
                    sportBias += tags[i].Count;
                }
                if (TagGenres.AnimeTags.Contains(tags[i].Name))
                {
                    animeBias += tags[i].Count;
                }
                if (TagGenres.ExotericTags.Contains(tags[i].Name))
                {
                    exotericBias += tags[i].Count;
                }
            }


            double totalWeight = sportBias + animeBias + exotericBias;
            double sportPercent = ((sportBias) * 100) / totalWeight;
            double animePercent = ((animeBias) * 100) / totalWeight;
            double exotericPercent = ((exotericBias) * 100) / totalWeight;


            if (sportPercent > animePercent && sportPercent > exotericPercent)
            {
                profile.Genre = "sport";
            }
            else if (animePercent > sportPercent && animePercent > exotericPercent)
            {
                profile.Genre = "anime";
            }
            else if (exotericPercent > animePercent && exotericPercent > sportPercent)
            {
                profile.Genre = "exoteric";
            }
            else
            {
                profile.Genre = "generic";
            }


            return profile;
        }

        public async Task<DataResponse<Product>> GetGifts(List<TagWithCount> tags, string username)
        {
            InstagramProfile profile = CategorizeProfileByTags(tags, username);


            return await _ProductService.GetByGenre(profile.Genre);

        }

    }
}
