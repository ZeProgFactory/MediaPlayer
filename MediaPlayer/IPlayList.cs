using System;
using System.Collections.Generic;
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

   public interface IPlaylist : IList<IMediaItem>
   {
      //event MediaItemChangedEventHandler MediaItemChanged;

      RepeatMode RepeatMode { get; set; }
      ShuffleMode ShuffleMode { get; set; }

      IMediaItem Current { get; set; }

      Task PlayPrevious();
      Task PlayNext();
      void PlayByPosition(int ind);
      void AddRange(List<MediaItem> playList);
   }
}


