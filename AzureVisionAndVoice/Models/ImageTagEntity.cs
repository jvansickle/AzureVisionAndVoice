using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media.Abstractions;

namespace AzureVisionAndVoice.Models
{
    public class ImageTagEntity
    {
        public MediaFile Image { get; set; }
        public IEnumerable<ImageTag> Tags { get; set; }
    }
}
