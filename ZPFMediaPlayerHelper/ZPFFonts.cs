#if WPF
using System.Windows.Media;
#endif

#if XF
#endif

using System;

namespace ZPFFonts
{
#if WPF
   class IF_Dummy { }
#endif

#if XF
   public static partial class MPF
#endif
#if WPF
   public sealed class MPF
#endif
   {
      //public static System.Windows.Media.FontFamily FontFamily = new System.Windows.Media.FontFamily(new Uri("pack://application:,,,"), "/ZPFFonts;component/Fonts/#IconFont");

      //public static Image DrawText(string text, float FontSize = 12)
      //{
      //   System.Windows.Media.FontFamily FontFamily2 = new System.Windows.Media.FontFamily(new System.Uri("pack://application:,,,"), GetFamilyName());

      //   Color backColor = Color.Transparent;
      //   Color textColor = Color.Black;
      //   FontFamily FontFamily = new System.Drawing.FontFamily(GetFamilyName());

      //   Font font = new Font(FontFamily, FontSize);

      //   return DrawText(text, font, textColor, backColor);
      //}

      //public static Image DrawText(string text, Font font, Color textColor, Color backColor)
      //{
      //   //first, create a dummy bitmap just to get a graphics object
      //   Image img = new Bitmap(1, 1);
      //   Graphics drawing = Graphics.FromImage(img);

      //   //measure the string to see how big the image needs to be
      //   SizeF textSize = drawing.MeasureString(text, font);

      //   //free up the dummy image and old graphics object
      //   img.Dispose();
      //   drawing.Dispose();

      //   //create a new image of the right size
      //   img = new Bitmap((int)textSize.Width, (int)textSize.Height);

      //   drawing = Graphics.FromImage(img);

      //   //paint the background
      //   drawing.Clear(backColor);

      //   //create a brush for the text
      //   Brush textBrush = new SolidBrush(textColor);

      //   drawing.DrawString(text, font, textBrush, 0, 0);

      //   drawing.Save();

      //   textBrush.Dispose();
      //   drawing.Dispose();

      //   return img;
      //}

      //public Bitmap ConvertTextToImage(string txt, string fontname, int fontsize, Color bgcolor, Color fcolor, int width, int Height)
      //{
      //   Bitmap bmp = new Bitmap(width, Height);

      //   using (Graphics graphics = Graphics.FromImage(bmp))
      //   {

      //      Font font = new Font(fontname, fontsize);
      //      graphics.FillRectangle(new SolidBrush(bgcolor), 0, 0, bmp.Width, bmp.Height);
      //      graphics.DrawString(txt, font, new SolidBrush(fcolor), 0, 0);
      //      graphics.Flush();
      //      font.Dispose();
      //      graphics.Dispose();
      //   }

      //   return bmp;
      //}

#if XF

      public static string GetContent(int icon)
      {
         return "" + (char)(icon);
      }

      public static string GetContent(string icon)
      {
         return icon;
      }

      public static string GetFontFamily(string RuntimePlatform)
      {
         // SKTypeface.FromStream(XFHelper.GetStreamFromResources(typeof(PageEx), "ZPF_Basics_XF.Images.IconFont.ttf"));

         switch (RuntimePlatform)
         {
            case "macOS":
            case "iOS":
               // Add the font file with Build Action: BundleResource, and
               // Update the Info.plist file(Fonts provided by application, or UIAppFonts, key)
               return "IconFont";

            case "Android":
               // add the font file to the Assets folder in the application project and set Build Action: AndroidAsset. 
               return "IconFont.ttf#IconFont";

            case "WPF":
               // https://github.com/xamarin/Xamarin.Forms/pull/3225
               // add the font file to the /Fonts/ folder in the application project and set the Build Action:Resource - Do not copy.
               //_LabelImage.FontFamily = $"component/Fonts/#IconFont"; break;
               //_LabelImage.FontFamily = $"/Assets/IconFont.ttf#IconFont";
               return "/StockAPPro.WPF;component/Assets/#IconFont";

            default:
            case "UWP":
               // add the font file to the /Assets/Fonts/ folder in the application project and set the Build Action:Content.
               //return "Assets/Fonts/IconFont.ttf#IconFont";
               return "/ZPF_Basics_XF;component/Images/IconFont.ttf#IconFont";
         };
      }
#endif

#if WPF
      public static string GetFamilyName()
      {
         IF_Dummy o = new IF_Dummy();
         string aName = System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetAssembly(o.GetType()).Location);

         string st = $"/{aName};component/Fonts/#IconFont";

         return st;
      }

      private static System.Windows.Media.FontFamily _FontFamily = null;
      public static System.Windows.Media.FontFamily FontFamily
      {
         get
         {
            if (_FontFamily == null)
            {
               _FontFamily = new System.Windows.Media.FontFamily(new System.Uri("pack://application:,,,"), GetFamilyName());
            };

            return _FontFamily;
         }
      }

      private static System.Windows.Media.Typeface _Typeface = null;

      /// <summary>
      /// Creates a new System.Windows.Media.ImageSource of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
      /// </summary>
      /// <param name="icon">The FontAwesome icon to be drawn.</param>
      /// <param name="foregroundBrush">The System.Windows.Media.Brush to be used as the foreground.</param>
      /// <returns>A new System.Windows.Media.ImageSource</returns>
      public static System.Windows.Media.ImageSource GetImageSource(string icon, System.Windows.Media.Brush foregroundBrush, double emSize = 100, double margin = 0)
      {
         return GetImageSource(icon[0], foregroundBrush, emSize, margin);
      }

      public static System.Windows.Media.ImageSource GetImageSource(int icon, System.Windows.Media.Brush foregroundBrush, double emSize = 100, double margin = 0)
      {
         string charIcon = char.ConvertFromUtf32(icon);

         if (_Typeface == null)
         {
            _Typeface = new System.Windows.Media.Typeface(FontFamily, System.Windows.FontStyles.Normal, System.Windows.FontWeights.Normal, System.Windows.FontStretches.Normal);
         };

         System.Windows.Media.DrawingVisual visual = new System.Windows.Media.DrawingVisual();

         using (System.Windows.Media.DrawingContext drawingContext = visual.RenderOpen())
         {
            FormattedText ft = new System.Windows.Media.FormattedText(
                   charIcon,
                   System.Globalization.CultureInfo.InvariantCulture,
                   System.Windows.FlowDirection.LeftToRight,
                   _Typeface, emSize - (2 * margin), foregroundBrush);

            ft.TextAlignment = System.Windows.TextAlignment.Center;

            drawingContext.DrawRectangle(null, new Pen(Brushes.Black, 0), new System.Windows.Rect(0, 0, emSize, emSize));
            drawingContext.DrawText(ft, new System.Windows.Point(emSize / 2, margin));
         };

         return new System.Windows.Media.DrawingImage(visual.Drawing);
      }

      public static string GetContent(int icon)
      {
         return "" + (char)(icon);
      }

#endif

      public static string Media_Backward { get => "" + (char)0xE900; }
      public static string Media_First { get => "" + (char)0xE901; }
      public static string Media_Last { get => "" + (char)0xE902; }
      public static string Media_Next { get => "" + (char)0xE903; }
      public static string Media_Pause { get => "" + (char)0xE904; }
      public static string Media_Previous { get => "" + (char)0xE905; }
      public static string Media_Fast_Forward { get => "" + (char)0xE906; }
      public static string Media_Play_01 { get => "" + (char)0xE907; }
      public static string Music { get => "" + (char)0xE908; }
      public static string Radio { get => "" + (char)0xE909; }
      public static string Rating_01 { get => "" + (char)0xE90A; }
      public static string Rating_02 { get => "" + (char)0xE90B; }
      public static string Rating_03 { get => "" + (char)0xE90C; }
      public static string RSSFeeds { get => "" + (char)0xE90D; }
      public static string ZPF { get => "" + (char)0xE90E; }
   }
}
