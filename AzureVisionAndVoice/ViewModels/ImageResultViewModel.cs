using System.Collections.ObjectModel;
using AzureVisionAndVoice.Models;
using Xamarin.Forms;

namespace AzureVisionAndVoice.ViewModels
{
    public class ImageResultViewModel : ViewModel
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

        ObservableCollection<ImageTag> _tags;
        public ObservableCollection<ImageTag> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                NotifyPropertyChanged();
            }
        }
    }
}
