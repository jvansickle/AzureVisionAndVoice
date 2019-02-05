using AzureVisionAndVoice.ViewModels;
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

            BindingContext = new ImageCaptureViewModel();
        }
    }
}
