using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZPF.Media
{
   class Playlist : IPlaylist
   {
      public RepeatMode RepeatMode { get; set; }
      public ShuffleMode ShuffleMode { get; set; }

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
