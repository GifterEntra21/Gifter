using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Shared;

namespace NeuralNetworkLayer
{
    public class RecommendationModel
    {
        internal static InstagramProfile CategorizeProfileByTags(List<TagWithCount> tags, string userName)
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

        public static List<Product> GetGifts(List<TagWithCount> tags, string username)
        {
            InstagramProfile profile = CategorizeProfileByTags(tags, username);
            List<Product> gifts = new List<Product>();


            if (profile.Genre == "anime")
            {
                //gifts = ProductsDAL.GetProducts where genre == anime
            }
            else if (profile.Genre == "exoteric")
            {
                //gifts = ProductsDAL.GetProducts where genre == exoteric
            }
            else if (profile.Genre == "sport")
            {
                //gifts = ProductsDAL.GetProducts where genre == sport
            }
            else
            {
                //gifts = ProductsDAL.GetProducts where genre == generic
            }

            return gifts;
        }

    }
}
