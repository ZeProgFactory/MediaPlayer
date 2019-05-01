using System;
using System.Threading.Tasks;

namespace ZPF.Media
{
   public class MediaPlayerImplementation : MediaPlayerBase
   {
      public override MediaPlayerState State => throw new System.NotImplementedException();

      public override TimeSpan Position => throw new NotImplementedException();

      public override TimeSpan Duration => throw new NotImplementedException();

      public override TimeSpan Buffered => throw new NotImplementedException();

      public override IMediaExtractor MediaExtractor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

      public override void Init()
      {
         throw new System.NotImplementedException();
      }

      public override Task Pause()
      {
         throw new NotImplementedException();
      }

      public override Task<IMediaItem> Play(string uri)
      {
         throw new System.NotImplementedException();
      }

      public override Task Play()
      {
         throw new NotImplementedException();
      }

      public override Task Play(IMediaItem mediaItem)
      {
         throw new NotImplementedException();
      }

      public override Task SeekTo(TimeSpan position)
      {
         throw new NotImplementedException();
      }

      public override Task Stop()
      {
         throw new System.NotImplementedException();
      }
   }
}
