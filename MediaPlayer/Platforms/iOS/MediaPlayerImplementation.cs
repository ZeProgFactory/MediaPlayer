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
         _player?.Player.Pause();

         return Task.CompletedTask;
      }

      private NSObject _observer;

      private void RemoveStatusObserver()
      {
         if (_observer != null)
         {
            try
            {
               _player?.Player?.CurrentItem?.RemoveObserver(_observer, "status");
            }
            catch { }
            finally
            {

               _observer = null;
            }
         }
      }

      private void ObserveStatus(NSObservedChange e)
      {
         if (e.NewValue != null)
         {
            switch (_player.Player.Status)
            {
               case AVPlayerStatus.Failed:
                  break;

               case AVPlayerStatus.ReadyToPlay:
                  break;

               case AVPlayerStatus.Unknown:
                  break;
            };

            //if (_player.Player.Status == AVPlayerStatus.ReadyToPlay)
            //{
            //   Element?.RaiseMediaOpened();
            //}

            System.Diagnostics.Debug.WriteLine("*** " + DateTimeOffset.Now + " " + e.NewValue.ToString());
         }
      }

      public override async Task<IMediaItem> Play(string uri)
      {
         var mediaItem = await MediaExtractor.CreateMediaItem(uri);

         NSError err;


         AVAsset asset = null;
         asset = AVUrlAsset.Create(NSUrl.FromString(uri));
         AVPlayerItem item = new AVPlayerItem(asset);

         // AVPlayerItem.TimeJumpedNotification
         // AVPlayerItem.DidPlayToEndTimeNotification

         _observer = (NSObject)item.AddObserver("status", NSKeyValueObservingOptions.New, ObserveStatus);

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
         _player?.Player.Play();

         return Task.CompletedTask;
      }

      public override Task Play(IMediaItem mediaItem)
      {
         throw new NotImplementedException();
      }

      public override Task SeekTo(TimeSpan position)
      {
         _player?.Player.SeekAsync(new CoreMedia.CMTime((long)position.TotalMilliseconds, 1000));

         return Task.CompletedTask;
      }

      public override Task Stop()
      {
         //ToDo: iOS: Stop()
         _player?.Player.Pause();

         return Task.CompletedTask;
      }
   }
}
