using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Plugin.Media.Abstractions;

namespace AzureVisionAndVoice.CognitiveServices
{
    public static class FaceDetection
    {
        public static async Task<IList<DetectedFace>> ProcessImageAsync(MediaFile image)
        {
            var subscriptionKey = CognitiveKey.GetFaceKey();

            // Replace or verify the region.
            //
            // You must use the same region as you used to obtain your subscription
            // keys. For example, if you obtained your subscription keys from the
            // westus region, replace "Westcentralus" with "Westus".
            //
            // NOTE: Free trial subscription keys are generated in the westcentralus
            // region, so if you are using a free trial subscription key, you should
            // not need to change this region.
            var faceEndpoint = "https://eastus.api.cognitive.microsoft.com";

            var faceClient = new FaceClient(
                    new ApiKeyServiceClientCredentials(subscriptionKey),
                    new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = faceEndpoint
            };

            // The list of detected faces.
            IList<DetectedFace> faceList;
            // The list of descriptions for the detected faces.
            //string[] faceDescriptions;
            // The resize factor for the displayed image.
            //double resizeFactor;

            // The list of Face attributes to return.
            IList<FaceAttributeType> faceAttributes =
                new FaceAttributeType[]
                {
            FaceAttributeType.Gender, FaceAttributeType.Age,
            FaceAttributeType.Smile, FaceAttributeType.Emotion,
            FaceAttributeType.Glasses, FaceAttributeType.Hair
                };

            // Call the Face API.
            try
            {
                using (Stream imageFileStream = image.GetStream())
                {
                    // The second argument specifies to return the faceId, while
                    // the third argument specifies not to return face landmarks.
                    faceList = await faceClient.Face.DetectWithStreamAsync(
                        imageFileStream, true, false, faceAttributes);
                }
            }
            // Catch and display Face API errors.
            catch (APIErrorException e)
            {
                // TODO Do better
                Console.WriteLine(e);
                faceList = new List<DetectedFace>();
            }
            // Catch and display all other errors.
            catch (Exception e)
            {
                // TODO Do better
                Console.WriteLine(e);
                faceList = new List<DetectedFace>();
            }

            return faceList;
        }
    }
}
