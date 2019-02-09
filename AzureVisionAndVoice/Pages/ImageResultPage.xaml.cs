using AzureVisionAndVoice.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AzureVisionAndVoice.Pages
{
    public partial class ImageResultPage : ContentPage
    {
        public ImageResultPage()
        {
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is ImageResultViewModel vm)
            {
                vm.ResultSaved += async () =>
                {
                    await Navigation.PopAsync();
                };
            }
        }
    }
}
