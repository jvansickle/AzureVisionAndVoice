using System.IO;
using System.Reflection;

namespace AzureVisionAndVoice.CognitiveServices
{
    public static class CognitiveKey
    {
        public static string GetCognitiveKey()
        {
            var assembly = typeof(VisionService).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("AzureVisionAndVoice.Keys.ComputerVisionKey.txt");
            string subscriptionKey;
            using (var reader = new StreamReader(stream))
            {
                subscriptionKey = reader.ReadToEnd();
            }
            return subscriptionKey;
        }

        public static string GetFaceKey()
        {
            var assembly = typeof(VisionService).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("AzureVisionAndVoice.Keys.FaceKey.txt");
            string subscriptionKey;
            using (var reader = new StreamReader(stream))
            {
                subscriptionKey = reader.ReadToEnd();
            }
            return subscriptionKey;
        }
    }
}
