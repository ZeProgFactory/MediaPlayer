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

      public IMediaItem Current
      {
         get => _Current;
         set
         {
            if (_Current != value)
            {
               _Current = value;

               if (this.IndexOf(_Current) < 0)
               {
                  this.Add(_Current);
               };
            };
         }
      }
      IMediaItem _Current = null;

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
