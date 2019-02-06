using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AzureVisionAndVoice.ViewModels
{
    public class ImageCaptureViewModel : ViewModel
    {
        public event Action<MediaFile, IEnumerable<ImageTag>> ImageAnalyzed;

        MediaFile _image;
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

        bool _isAnalyzing;
        public bool Analyzing
        {
            get => _isAnalyzing;
            set
            {
                _isAnalyzing = value;
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
                        _image = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            DefaultCamera = CameraDevice.Rear,
                            PhotoSize = PhotoSize.MaxWidthHeight,
                            SaveToAlbum = false,
                            ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen
                        });

                        if (_image != null)
                            ImageSource = ImageSource.FromStream(_image.GetStream);
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
                        _image = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                        {
                            ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen
                        });

                        if (_image != null)
                            ImageSource = ImageSource.FromStream(_image.GetStream);
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
                        Analyzing = true;
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ImageCaptureViewModel)).Assembly;
                        Stream stream = assembly.GetManifestResourceStream("AzureVisionAndVoice.Keys.ComputerVisionKey.txt");
                        string subscriptionKey;
                        using (var reader = new StreamReader(stream))
                        {
                            subscriptionKey = reader.ReadToEnd();
                        }

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
                        using (Stream imageStream = _image.GetStream())
                        {
                            var analysis = await computerVision.AnalyzeImageInStreamAsync(imageStream, features);

                            Analyzing = false;
                            ImageAnalyzed?.Invoke(_image, analysis.Tags);
                        }
                    });
                }

                return _analyzeImage;
            }
        }
    }
}
