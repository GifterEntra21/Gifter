using Entities;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLayer.Interfaces
{
    public interface IRecommendationModel
    {
        public Task<SingleResponse<InstagramProfile>> CategorizeProfileByTags(List<TagWithCount> tags, string username);
    }
}
