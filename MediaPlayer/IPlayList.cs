using System;
using System.Threading.Tasks;
using System.Timers;

namespace ZPF.Media
{
   public enum ShuffleMode
   {
      Off = 0,
      All = 1
   }

   public enum RepeatMode
   {
      Off = 0,
      One = 1,
      All = 2
   }

   //public delegate void MediaItemChangedEventHandler(object sender, MediaItemEventArgs e);

   public interface IPlayList
   {
      //event MediaItemChangedEventHandler MediaItemChanged;

      RepeatMode RepeatMode { get; set; }
      ShuffleMode ShuffleMode { get; set; }

      Task PlayPrevious();
      Task PlayNext();
   }
}


