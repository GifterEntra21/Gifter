using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace NeuralNetworkLayer
{
    internal class RecommendationModel
    {
        //Tag will be an externalclass by itself -- to be implemented
        public static string CategorizeProfileByTags(List<string> tags)
        {
            InstagramProfile profile = new();

            int sportBias = 0;
            int exotericBias = 0;
            int animeBias = 0;

            for (int i = 0; i < tags.Count; i++)
            {

            }


            return "";
        }


    }
}
