using System;
using System.Threading.Tasks;
using AVFoundation;
using AVKit;
using Foundation;

namespace ZPF.Media
{
   public class MediaPlayerImplementation : MediaPlayerBase
   {
      public override object Player { get => _player; }
      //private AVAudioPlayer _player = null;
      private AVPlayerViewController _player = new AVPlayerViewController();

      public override IMediaExtractor MediaExtractor { get => _MediaExtractor; set => _MediaExtractor = value; }
      private IMediaExtractor _MediaExtractor;

      public MediaPlayerImplementation()
      {
         _MediaExtractor = new MediaExtractor();
      }

      public override MediaPlayerState State => throw new System.NotImplementedException();

      public override TimeSpan Position => throw new NotImplementedException();

      public override TimeSpan Duration => throw new NotImplementedException();

      public override TimeSpan Buffered => throw new NotImplementedException();

      // - - -  - - - 

      public override decimal Volume
      {
         get
         {
            return _Volume;
         }
         set
         {
            _Volume = value;

            if (_Muted)
            {
               // Nothing to do
            }
            else
            {
               //_player.Volume = (float)_Volume;
            };
         }
      }
      decimal _Volume = 1;

      public override decimal Balance
      {
         get
         {
            throw new NotImplementedException();
         }
         set
         {
            throw new NotImplementedException();
         }
      }

      public override bool Muted
      {
         get
         {
            return _Muted;
         }
         set
         {
            _Muted = value;

            if (_Muted)
            {
               //_player.Volume = 0;
            }
            else
            {
               //_player.Volume = (float)_Volume;
            };
         }
      }
      bool _Muted = false;

      // - - -  - - - 

      public override void Init()
      {
         throw new System.NotImplementedException();
      }

      public override Task Pause()
      {
         throw new NotImplementedException();
      }

      public override async Task<IMediaItem> Play(string uri)
      {
         var mediaItem = await MediaExtractor.CreateMediaItem(uri);

         NSError err;

         //// Any existing music?
         //if (_player != null)
         //{
         //   // Stop and dispose of any music
         //   _player.Stop();
         //   _player.Dispose();
         //}

         //// Initialize music
         //if (_player == null)
         //{
         //   var url = new NSUrl(uri);
         //   _player = AVAudioPlayer.FromUrl(url);
         //   //_player = new AVAudioPlayer(uri, "wav", out err);
         //   _player.Play();
         //};

         AVAsset asset = null;
         asset = AVUrlAsset.Create(NSUrl.FromString(uri));
         AVPlayerItem item = new AVPlayerItem(asset);

         if (_player.Player != null)
         {
            _player.Player.ReplaceCurrentItemWithPlayerItem(item);
         }
         else
         {
            _player.Player = new AVPlayer(item);
         }

         _player.Player.Play();

         return mediaItem;
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
