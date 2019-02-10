using System;
using System.Globalization;
using System.Linq;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Xamarin.Forms;

namespace AzureVisionAndVoice.Converters
{
    public class PrimaryEmotionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Emotion emotion)
            {
                string strongestEmotion = string.Empty;
                double strongestValue = 0;
                emotion.GetType().GetProperties().ToList().ForEach(property =>
                {
                    if (string.IsNullOrEmpty(strongestEmotion))
                    {
                        strongestEmotion = property.Name;
                        strongestValue = (double)property.GetValue(emotion);
                    }
                    else if (strongestValue < (double)property.GetValue(emotion))
                    {
                        strongestEmotion = property.Name;
                        strongestValue = (double)property.GetValue(emotion);
                    }
                });

                return $"{strongestEmotion} ({strongestValue})";
            }

            throw new Exception();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
