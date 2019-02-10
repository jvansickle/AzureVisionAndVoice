using System;
using System.Globalization;
using AzureVisionAndVoice.ViewModels;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Xamarin.Forms;

namespace AzureVisionAndVoice.Converters
{
    public class IndexOfConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DetectedFace face)
            {
                if (parameter is FaceRecognitionDisplayViewModel vm)
                {
                    return vm.Faces.IndexOf(face);
                }
            }

            return "-1";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
