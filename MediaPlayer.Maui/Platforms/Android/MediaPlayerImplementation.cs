using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZPF.Media
{
   /// <summary>
   /// MediaPlayerImplementation for DroidS ...
   /// </summary>
   public class MediaPlayerImplementation : MediaPlayerBase
   {
      public override object Player => throw new NotImplementedException();

      public override IMediaExtractor MediaExtractor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

      public override MediaPlayerState State => throw new NotImplementedException();

      public override TimeSpan Position => throw new NotImplementedException();

      public override TimeSpan Duration => throw new NotImplementedException();

      public override TimeSpan Buffered => throw new NotImplementedException();

      public override decimal Volume { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public override decimal Balance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public override bool Muted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

      public override void Init(object mainWindow = null)
      {
         throw new NotImplementedException();
      }

      public override Task Pause()
      {
         throw new NotImplementedException();
      }

      public override Task<IMediaItem> Play(string uri)
      {
         throw new NotImplementedException();
      }

      public override Task Play(IMediaItem mediaItem)
      {
         throw new NotImplementedException();
      }

      public override Task Play()
      {
         throw new NotImplementedException();
      }

      public override Task SeekTo(TimeSpan position)
      {
         throw new NotImplementedException();
      }

      public override Task SetSource(IMediaItem mediaItem)
      {
         throw new NotImplementedException();
      }

      public override Task Stop()
      {
         throw new NotImplementedException();
      }
   }
}
