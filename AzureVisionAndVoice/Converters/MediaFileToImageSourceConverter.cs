using System;
using System.Globalization;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AzureVisionAndVoice.Converters
{
    public class MediaFileToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is MediaFile image)
            {
                return ImageSource.FromStream(image.GetStream);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
