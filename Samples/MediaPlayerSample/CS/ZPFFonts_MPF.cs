using System;

namespace ZPFFonts
{
#if XF
   public static partial class MPF
#endif
#if WPF
   public sealed class MPF
#endif
   {
#if XF

      public static string GetContent(int icon)
      {
         return "" + (char)(icon);
      }

      public static string GetContent(string icon)
      {
         return icon;
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
      public static string Media_Stop { get => "" + (char)0xE908; }
      public static string Music { get => "" + (char)0xE909; }
      public static string Radio { get => "" + (char)0xE90A; }
      public static string Rating_01 { get => "" + (char)0xE90B; }
      public static string Rating_02 { get => "" + (char)0xE90C; }
      public static string Rating_03 { get => "" + (char)0xE90D; }
      public static string RSSFeeds { get => "" + (char)0xE90E; }
      public static string ZPF { get => "" + (char)0xE90F; }

   }
}
