using System;
using System.Collections.Generic;
using System.ComponentModel;
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

      string Title { get; set; }
      RepeatMode RepeatMode { get; set; }
      ShuffleMode ShuffleMode { get; set; }

      /// <summary>
      /// Get the current track from the Queue
      /// </summary>
      IMediaItem Current { get; set; }

      Task PlayPrevious();
      Task PlayNext();
      void PlayByPosition(int ind);
      void AddRange(List<IMediaItem> playList);

      /// <summary>
      /// If the Queue has a next track
      /// </summary>
      bool HasNext();

      /// <summary>
      /// Get the next item from the queue
      /// </summary>
      IMediaItem NextItem { get; }

      /// <summary>
      /// If the Queue has a previous track
      /// </summary>
      bool HasPrevious();

      /// <summary>
      /// Get the previous item from the queue
      /// </summary>
      IMediaItem PreviousItem { get; }

      Task InsertAfterCurrent(IMediaItem mediaItem);
   }
}


