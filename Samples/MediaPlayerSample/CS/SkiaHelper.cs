using System;
using System.IO;
using System.Reflection;
using MediaPlayerSample;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace ZPF.XF
{
   class SkiaHelper
   {
      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - - 

      public static ImageSource SkiaFontIcon(string Icon, int size)
      {
         switch (Device.RuntimePlatform)
         {
            case Device.macOS:
            case Device.WPF:
               //ToDo: Xamarin.Essentials.DeviceDisplay.MainDisplayInfo not implemented
               break;

            case Device.UWP:
            case Device.iOS:
               // nope
               break;

            case Device.Android:
            default:
               // Get Metrics
               var mainDisplayInfo = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo;

               size = (int)(size * mainDisplayInfo.Density);
               break;
         };

         return Render2ImageSource(size, size, (SKImageInfo info, SKCanvas canvas) =>
         {
            canvas.Clear(SKColors.Transparent);
            // canvas.Scale(2);

            SKPaint paint = new SKPaint
            {
               Style = SKPaintStyle.Fill,
               Color = Color.Black.ToSKColor(),
               LcdRenderText = true,
               IsAntialias = true,
            };

            paint.Typeface = SKTypeface.FromStream(GetStreamFromResources(typeof(ZPFFonts.MPF), "MediaPlayerSample.Fonts.MediaPlayerFont.ttf"));
            paint.TextSize = info.Width;

            canvas.DrawText(ZPFFonts.MPF.GetContent(Icon), 0, info.Height, paint);
         });
      }

      public static ImageSource Render2ImageSource(int width, int height, Action<SKImageInfo, SKCanvas> action)
      {
         SKImageInfo info = new SKImageInfo(width, height);
         SKBitmap bitmap = new SKBitmap(info.Width, info.Height);
         SKCanvas canvas = new SKCanvas(bitmap);

         //Draw on canvas from stored commands DrawPath, etc.
         action(info, canvas);

         return (SKBitmapImageSource)bitmap;
      }

      public static Stream GetStreamFromResources(Type type, string resourceName)
      {
         var assembly = type.GetTypeInfo().Assembly;
         return assembly.GetManifestResourceStream(resourceName);
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - - 
   }
}
