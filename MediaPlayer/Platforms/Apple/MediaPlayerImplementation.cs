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

#if __IOS__
      private AVPlayerViewController _player = new AVPlayerViewController();
#else
      private AVPlayerView _player = new AVPlayerView();
#endif

      public override IMediaExtractor MediaExtractor { get => _MediaExtractor; set => _MediaExtractor = value; }
      private IMediaExtractor _MediaExtractor;

      public MediaPlayerImplementation()
      {
         _MediaExtractor = new MediaExtractor();

         //ToDo: events ...
         IsInitialized = true;
      }

      public override void Init(object mainWindow = null)
      {
         IsInitialized = true;
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
               _player.Player.Volume = (float)_Volume;
            };
         }
      }
      decimal _Volume = 1;

      public override decimal Balance
      {
         get
         {
            return 1;
         }
         set
         {
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
               _player.Player.Volume = 0;
            }
            else
            {
               _player.Player.Volume = (float)_Volume;
            };
         }
      }
      bool _Muted = false;

      // - - -  - - - 

      public override Task Pause()
      {
         _player?.Player.Pause();

         return Task.CompletedTask;
      }

      //private NSObject _observer;

      //private void RemoveStatusObserver()
      //{
      //   if (_observer != null)
      //   {
      //      try
      //      {
      //         _player?.Player?.CurrentItem?.RemoveObserver(_observer, "status");
      //      }
      //      catch { }
      //      finally
      //      {

      //         _observer = null;
      //      }
      //   }
      //}

      //private void ObserveStatus(NSObservedChange e)
      //{
      //   if (e.NewValue != null)
      //   {
      //      switch (_player.Player.Status)
      //      {
      //         case AVPlayerStatus.Failed:
      //            break;

      //         case AVPlayerStatus.ReadyToPlay:
      //            break;

      //         case AVPlayerStatus.Unknown:
      //            break;
      //      };

      //      //if (_player.Player.Status == AVPlayerStatus.ReadyToPlay)
      //      //{
      //      //   Element?.RaiseMediaOpened();
      //      //}

      //      System.Diagnostics.Debug.WriteLine("*** " + DateTimeOffset.Now + " " + e.NewValue.ToString());
      //   }
      //}

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - - 

      public override async Task<IMediaItem> Play(string uri)
      {
         var mediaItem = await MediaExtractor.CreateMediaItem(uri);

         await Play(mediaItem);

         return mediaItem;
      }

      public override async Task Play(IMediaItem mediaItem)
      {
         if (!mediaItem.IsMetadataExtracted)
         {
            mediaItem = await MediaExtractor.CreateMediaItem(mediaItem);
         };

         Playlist.Current = mediaItem;

         //await SetSource(mediaItem);
         //await Play();
      }

      public override async Task SetSource(IMediaItem mediaItem)
      {
         if (!mediaItem.IsMetadataExtracted)
         {
            mediaItem = await MediaExtractor.CreateMediaItem(mediaItem);
         };

         AVAsset asset = null;
         asset = AVUrlAsset.Create(NSUrl.FromString(mediaItem.MediaUri));
         AVPlayerItem item = new AVPlayerItem(asset);

         // AVPlayerItem.TimeJumpedNotification
         // AVPlayerItem.DidPlayToEndTimeNotification

         //_observer = (NSObject)item.AddObserver("status", NSKeyValueObservingOptions.New, ObserveStatus);

         if (_player.Player != null)
         {
            _player.Player.ReplaceCurrentItemWithPlayerItem(item);
         }
         else
         {
            _player.Player = new AVPlayer(item);
         }
      }

      public override Task Play()
      {
         _player?.Player.Play();

         return Task.CompletedTask;
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - - 

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
