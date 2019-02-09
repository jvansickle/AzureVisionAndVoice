using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media.Abstractions;

namespace AzureVisionAndVoice.CognitiveServices
{
    public static class VisionTags
    {
        public static async Task<ImageAnalysis> GetTagsAsync(MediaFile image)
        {
            var subscriptionKey = CognitiveKey.GetCognitiveKey();

            // Specify the features to return
            List<VisualFeatureTypes> features = new List<VisualFeatureTypes> { VisualFeatureTypes.Tags };

            ComputerVisionClient computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subscriptionKey),
                new System.Net.Http.DelegatingHandler[] { })
            {

                // You must use the same region as you used to get your subscription
                // keys. For example, if you got your subscription keys from westus,
                // replace "westcentralus" with "westus".

                // Specify the Azure region
                Endpoint = "https://eastus.api.cognitive.microsoft.com"
            };

            // Analyze Images
            using (Stream imageStream = image.GetStream())
            {
                var analysis = await computerVision.AnalyzeImageInStreamAsync(imageStream, features);
                return analysis;
            }
        }
    }
}
