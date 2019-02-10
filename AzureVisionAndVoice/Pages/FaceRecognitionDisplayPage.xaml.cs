using System;
using System.Linq;
using AzureVisionAndVoice.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace AzureVisionAndVoice.Pages
{
    public partial class FaceRecognitionDisplayPage : ContentPage
    {
        FaceRecognitionDisplayViewModel ViewModel => BindingContext as FaceRecognitionDisplayViewModel;

        public FaceRecognitionDisplayPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            ViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(FaceRecognitionDisplayViewModel.Image) ||
                    args.PropertyName == nameof(FaceRecognitionDisplayViewModel.Faces))
                {
                    canvasView.InvalidateSurface();
                }
            };
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            // Make image into bitmap
            SKBitmap bitmap = SKBitmap.Decode(ViewModel.Image.GetStreamWithImageRotatedForExternalStorage());

            // Find rectangle to fit bitmap
            float scale = Math.Min((float)info.Width / bitmap.Width,
                                   (float)info.Height / bitmap.Height);
            SKRect rect = SKRect.Create(scale * bitmap.Width,
                                        scale * bitmap.Height);
            float x = (info.Width - rect.Width) / 2;
            float y = (info.Height - rect.Height) / 2;
            rect.Offset(x, y);

            // Draw the image
            canvas.DrawBitmap(bitmap, rect);

            // Prep rectangle paint
            var paint = new SKPaint
            {
                Color = new SKColor(255, 0, 0),
                IsStroke = true,
                StrokeWidth = 2
            };

            // Draw the face boxes
            ViewModel.Faces.ToList().ForEach(face =>
            {
                var faceRect = SKRect.Create(scale * face.FaceRectangle.Left + x,
                                             scale * face.FaceRectangle.Top + y,
                                             scale * face.FaceRectangle.Width,
                                             scale * face.FaceRectangle.Height);

                canvas.DrawRect(faceRect, paint);
            });
        }
    }
}
