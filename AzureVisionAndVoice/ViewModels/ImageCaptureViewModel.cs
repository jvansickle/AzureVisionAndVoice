using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AzureVisionAndVoice.ViewModels
{
    public class ImageCaptureViewModel : ViewModel
    {
        public event Action<ImageSource, IEnumerable<ImageTag>> ImageAnalyzed;

        MediaFile _photo;
        ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                NotifyPropertyChanged();
            }
        }

        Command _takePhoto;
        public Command TakePhoto
        {
            get
            {
                if (_takePhoto == null)
                {
                    _takePhoto = new Command(async () =>
                    {
                        _photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            DefaultCamera = CameraDevice.Rear,
                            PhotoSize = PhotoSize.MaxWidthHeight,
                            SaveToAlbum = false,
                            ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen
                        });

                        if (_photo != null)
                            ImageSource = ImageSource.FromStream(_photo.GetStream);
                    });
                }

                return _takePhoto;
            }
        }

        Command _selectPhoto;
        public Command SelectPhoto
        {
            get
            {
                if (_selectPhoto == null)
                {
                    _selectPhoto = new Command(async () =>
                    {
                        _photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                        {
                            ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen
                        });

                        if (_photo != null)
                            ImageSource = ImageSource.FromStream(_photo.GetStream);
                    });
                }

                return _selectPhoto;
            }
        }

        Command _analyzeImage;
        public Command AnalyzeImage
        {
            get
            {
                if (_analyzeImage == null)
                {
                    _analyzeImage = new Command(async () =>
                    {
                        // TODO Communicate with Azure
                        var subscriptionKey = "<key here>";

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
                        using (Stream imageStream = _photo.GetStream())
                        {
                            var analysis = await computerVision.AnalyzeImageInStreamAsync(imageStream, features);

                            ImageAnalyzed?.Invoke(ImageSource, analysis.Tags);
                        }
                    });
                }

                return _analyzeImage;
            }
        }
    }
}
