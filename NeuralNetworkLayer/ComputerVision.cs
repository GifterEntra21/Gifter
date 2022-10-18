using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System.Text;
using Shared.Responses;

namespace NeuralNetworkLayer
{
    public class ComputerVision
    {
        // Endpoint (URL) e chave de acesso (subscription key) do Computer Vision
        static string subscriptionKey = "ffef98b421584701a97a535cef29a294";
        static string endpoint = "https://gifter-cv.cognitiveservices.azure.com/";

        // URL para a análise de uma única imagem
        //private const string ANALYZE_URL_IMAGE = "https://cdn-1.motorsport.com/images/amp/0k783Oq0/s800/lewis-hamilton-mercedes-w13-cr.jpg";

        public async Task<DataResponse<ImageTag>> CheckTags(List<string> urls)
        {
            // Create a client
            try
            {
                if (urls == null)
                {
                    return ResponseFactory.CreateInstance().CreateSuccessDataResponse<ImageTag>(new List<ImageTag>());
                }
                ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);

                /*
                 * ANALISAR SOMENTE UMA IMAGEM
                 */
                //AnalyzeImageUrl(client, ANALYZE_URL_IMAGE).Wait();

                /*
                 * ANALISAR VÁRIAS IMAGENS
                 */

                List<ImageTag> tags = new List<ImageTag>();
                List<ImageAnalysis> results = new List<ImageAnalysis>();

                foreach (string url in urls)
                {
                    ImageAnalysis img = await AnalyzeImageUrl(client, url);

                    results.Add(img); // nesse caso, analisa as imagens dentro da lista de URLs
                    foreach (var image in results)
                    {
                        foreach (var tag in image.Tags)
                        {
                            tags.Add(tag);
                        }
                    }
                }

                return ResponseFactory.CreateInstance().CreateSuccessDataResponse<ImageTag>(tags);
            }

            catch (Exception ex)
            {

                return ResponseFactory.CreateInstance().CreateFailedDataResponse<ImageTag>(ex);
            }

            
        }
        /*
             * AUTHENTICATE
             * Creates a Computer Vision client used by each example.
             */
        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
            {
                Endpoint = endpoint
            };
            return client;
        }

        public async Task<ImageAnalysis> AnalyzeImageUrl(ComputerVisionClient client, string imageUrl)
        {
            // Cria uma lista para definir quais características serão extraídas da foto
            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Categories, VisualFeatureTypes.Description, VisualFeatureTypes.Faces,
                VisualFeatureTypes.ImageType, VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                VisualFeatureTypes.Color, VisualFeatureTypes.Brands, VisualFeatureTypes.Objects
            };

            //Console.WriteLine($"Analyzing the image {Path.GetFileName(imageUrl)}...");

            // Analisa a imagem do URL passado
            return await client.AnalyzeImageAsync(imageUrl, visualFeatures: features);
        }
    }
}