using System;
using System.Collections.ObjectModel;
using AzureVisionAndVoice.Repositories;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AzureVisionAndVoice.ViewModels
{
    public class ImageResultViewModel : ViewModel
    {
        public event Action ResultSaved;

        public MediaFile Image { get; set; }

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

        Command _saveResult;
        public Command SaveResult
        {
            get
            {
                if (_saveResult == null)
                {
                    _saveResult = new Command(() =>
                    {
                        ImageTagsRepository.SaveImageTag(Image, Tags);
                        ResultSaved?.Invoke();
                    });
                }

                return _saveResult;
            }
        }
    }
}
