using System;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AzureVisionAndVoice.ViewModels
{
    public class ImageCaptureViewModel : ViewModel
    {
        public event Action<MediaFile> ImageConfirmed;

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
                            PhotoSize = PhotoSize.Full,
                            SaveToAlbum = false,
                            ModalPresentationStyle = MediaPickerModalPresentationStyle.FullScreen
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

        Command _confirmImage;
        public Command ConfirmImage
        {
            get
            {
                if (_confirmImage == null)
                {
                    _confirmImage = new Command(() =>
                    {
                        ImageConfirmed.Invoke(_image);
                    });
                }

                return _confirmImage;
            }
        }
    }
}
