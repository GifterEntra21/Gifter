using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLayer.Interfaces
{
    public interface IComputerVision
    {
        public Task<DataResponse<ImageTag>> CheckTags(List<string> urls);
    }
}
