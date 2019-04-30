using System;
using System.Threading.Tasks;

namespace ZPF.Media
{
   public interface IMediaPlayer
   {
      void Init();
      bool Play(string URL);
   }

   public class MediaPlayerBase : IMediaPlayer
   {
      public void Init()
      {
         throw new NotImplementedException();
      }

      public bool Play(string URL)
      {
         throw new NotImplementedException();
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -
   }
}


