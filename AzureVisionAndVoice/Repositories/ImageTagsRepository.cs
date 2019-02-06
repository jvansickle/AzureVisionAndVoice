using System.Collections.Generic;
using AzureVisionAndVoice.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media.Abstractions;

namespace AzureVisionAndVoice.Repositories
{
    public static class ImageTagsRepository
    {
        static readonly List<ImageTagEntity> imageTagEntities = new List<ImageTagEntity>();

        public static void SaveImageTag(MediaFile image, IEnumerable<ImageTag> tags)
        {
            imageTagEntities.Add(new ImageTagEntity { Image = image, Tags = tags });
        }
    }
}
