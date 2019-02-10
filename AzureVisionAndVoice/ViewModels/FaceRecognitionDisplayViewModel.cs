using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Plugin.Media.Abstractions;

namespace AzureVisionAndVoice.ViewModels
{
    public class FaceRecognitionDisplayViewModel : ViewModel
    {
        MediaFile _image;
        public MediaFile Image
        {
            get => _image;
            set
            {
                _image = value;
                NotifyPropertyChanged();
            }
        }

        IList<DetectedFace> _faces;
        public IList<DetectedFace> Faces
        {
            get => _faces;
            set
            {
                _faces = value;
                NotifyPropertyChanged();
            }
        }
    }
}
