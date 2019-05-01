using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ZPF.Media
{
   class Playlist : ObservableCollection<IMediaItem>, IPlaylist
   {
      public RepeatMode RepeatMode { get; set; }
      public ShuffleMode ShuffleMode { get; set; }

      public IMediaItem Current { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

      public void AddRange(List<MediaItem> playList)
      {
         throw new NotImplementedException();
      }

      public void PlayByPosition(int ind)
      {
         throw new NotImplementedException();
      }

      public Task PlayNext()
      {
         throw new NotImplementedException();
      }

      public Task PlayPrevious()
      {
         throw new NotImplementedException();
      }
   }
}
