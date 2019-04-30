using System.Threading.Tasks;

namespace ZPF.Media
{
   public class MediaPlayerImplementation : MediaPlayerBase
   {
      public override MediaPlayerState State => throw new System.NotImplementedException();

      public override void Init()
      {
         throw new System.NotImplementedException();
      }

      public override Task<IMediaItem> Play(string uri)
      {
         throw new System.NotImplementedException();
      }

      public override Task Stop()
      {
         throw new System.NotImplementedException();
      }
   }
}
