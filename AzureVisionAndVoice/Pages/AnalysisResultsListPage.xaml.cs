using System;
using AzureVisionAndVoice.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AzureVisionAndVoice.Pages
{
    public partial class AnalysisResultsListPage : ContentPage
    {
        public AnalysisResultsListPage()
        {
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);

            BindingContext = new AnalysisResultsListViewModel();
        }

        async void CloseModalPage(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }
}
