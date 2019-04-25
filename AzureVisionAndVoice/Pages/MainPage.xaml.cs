using System;
using System.Collections.ObjectModel;
using AzureVisionAndVoice.CognitiveServices;
using AzureVisionAndVoice.ViewModels;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Xamarin.Forms;

namespace AzureVisionAndVoice.Pages
{
    public partial class MainPage : ContentPage
    {
        MainViewModel ViewModel => BindingContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel();
        }

        async void OpenAnalyzeImage(object sender, EventArgs args)
        {
            var page = new ImageCapturePage();
            var vm = new ImageCaptureViewModel();
            vm.ImageConfirmed += async (image) =>
            {
                // Have the progress indicator displayed
                ViewModel.Analyzing = true;

                // Close the ImageCapturePage
                await Navigation.PopModalAsync();

                // Do analysis and display results page
                var tags = await VisionService.GetTagsAsync(image);
                var resultViewModel = new ImageResultViewModel
                {
                    Image = image,
                    ImageSource = ImageSource.FromStream(image.GetStream),
                    Tags = new ObservableCollection<ImageTag>(tags)
                };
                var resultPage = new ImageResultPage
                {
                    BindingContext = resultViewModel
                };

                await Navigation.PushAsync(resultPage);

                // Hide the progress indicator
                ViewModel.Analyzing = false;
            };

            page.BindingContext = vm;
            await Navigation.PushModalAsync(new NavigationPage(page));
        }

        async void OpenAnalysisResults(object sender, EventArgs args)
        {
            var page = new AnalysisResultsListPage();
            await Navigation.PushModalAsync(new NavigationPage(page));
        }

        async void OpenAnalyzeFaces(object sender, EventArgs args)
        {
            var page = new ImageCapturePage();
            var vm = new ImageCaptureViewModel();
            vm.ImageConfirmed += async (image) =>
            {
                // Ensure activity indicator is displaying
                ViewModel.Analyzing = true;

                // Close Image Capture Page
                await Navigation.PopModalAsync();

                // TODO Call face detection service
                var detectedFaces = await FaceDetection.ProcessImageAsync(image);

                var resultPage = new FaceRecognitionDisplayPage();
                var resultViewModel = new FaceRecognitionDisplayViewModel
                {
                    Faces = detectedFaces,
                    Image = image
                };

                resultPage.BindingContext = resultViewModel;

                await Navigation.PushAsync(resultPage);

                ViewModel.Analyzing = false;
            };

            page.BindingContext = vm;

            await Navigation.PushModalAsync(new NavigationPage(page));
        }

        async void OpenAbout(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AboutPage());
        }
    }
}
