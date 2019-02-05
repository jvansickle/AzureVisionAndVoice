using System;
using System.Collections.Generic;
using AzureVisionAndVoice.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AzureVisionAndVoice.ViewModels
{
    public class ImageCaptureViewModel : ViewModel
    {
        public event Action<ImageSource, IEnumerable<ImageTag>> ImageAnalyzed;

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
                        var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            DefaultCamera = CameraDevice.Rear,
                            PhotoSize = PhotoSize.MaxWidthHeight,
                            SaveToAlbum = false,
                            ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen
                        });

                        if (photo != null)
                            ImageSource = ImageSource.FromStream(photo.GetStream);
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
                        var photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                        {
                            ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen
                        });

                        if (photo != null)
                            ImageSource = ImageSource.FromStream(photo.GetStream);
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
                    _analyzeImage = new Command(() =>
                    {
                        // TODO Communicate with Azure

                        ImageAnalyzed?.Invoke(ImageSource, new List<ImageTag> { new ImageTag { Name = "Image", Confidence = .1 } });
                    });
                }

                return _analyzeImage;
            }
        }
    }
}
