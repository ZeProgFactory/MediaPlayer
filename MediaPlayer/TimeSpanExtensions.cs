using System;

namespace ZPFMediaPlayer
{
   public static class TimeSpanExtensions
   {
      public static string ToDisplay(this TimeSpan timeSpan)
      {
         var ts = timeSpan;

         if (ts == TimeSpan.Zero) return "";
         if (ts == TimeSpan.MinValue) return "";

         if (ts.Hours > 0)
         {
            return string.Format("{0}:{1:00}:{2:0#}", (int)ts.TotalHours, ts.Minutes, ts.Seconds);
         };

         return string.Format("{0}:{1:0#}.{2:0}", ts.Minutes, ts.Seconds, (int)(ts.Milliseconds / 100));
      }
   }
}
