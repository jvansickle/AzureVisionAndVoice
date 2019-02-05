using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AzureVisionAndVoice.ViewModels
{
    public class ImageCaptureViewModel : ViewModel
    {
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
    }
}
