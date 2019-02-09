using System;
using System.Collections.ObjectModel;
using AzureVisionAndVoice.ViewModels;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media.Abstractions;
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
        }

        async void CloseModalPage(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }
}
