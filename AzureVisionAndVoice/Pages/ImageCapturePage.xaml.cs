using System.Collections.ObjectModel;
using AzureVisionAndVoice.ViewModels;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AzureVisionAndVoice.Pages
{
    public partial class ImageCapturePage : ContentPage
    {
        public ImageCapturePage()
        {
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);

            var vm = new ImageCaptureViewModel();
            vm.ImageAnalyzed += (imageSource, tags) =>
            {
                var resultViewModel = new ImageResultViewModel
                {
                    ImageSource = imageSource,
                    Tags = new ObservableCollection<ImageTag>(tags)
                };
                var resultPage = new ImageResultPage
                {
                    BindingContext = resultViewModel
                };

                Navigation.PushAsync(resultPage);
            };

            BindingContext = vm;
        }
    }
}
