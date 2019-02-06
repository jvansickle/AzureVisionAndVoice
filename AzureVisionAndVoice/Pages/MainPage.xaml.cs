using System;
using Xamarin.Forms;

namespace AzureVisionAndVoice.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OpenAnalyzeImage(object sender, EventArgs args)
        {
            var page = new ImageCapturePage();
            await Navigation.PushModalAsync(new NavigationPage(page));
        }
    }
}
