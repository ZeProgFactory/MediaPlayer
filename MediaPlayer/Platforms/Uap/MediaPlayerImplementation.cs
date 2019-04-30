using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;

namespace ZPF.Media
{
   public class MediaPlayerImplementation : MediaPlayerBase
   {
      private readonly Windows.Media.Playback.MediaPlayer _player;
      private IMediaExtractor _MediaExtractor;

      public MediaPlayerImplementation()
      {
         _player = new Windows.Media.Playback.MediaPlayer();
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public override void Init()
      {
         // uap

         IsInitialized = true;
      }

      // - - -  - - - 

      public override MediaPlayerState State
      {
         get { return _State; }
         //ToDo: ME discuss with Martijn
         //private set
         //{
         //    _state = value;
         //    MediaManager.OnStateChanged(this, new StateChangedEventArgs(_state));
         //}
      }
      private MediaPlayerState _State;


      private MediaPlayerState GetMediaPlayerState()
      {
         //ToDo: ME:  Stopped ?, Loading ?, Failed ?

         switch (_player.PlaybackSession.PlaybackState)
         {
            case MediaPlaybackState.Buffering: return MediaPlayerState.Buffering;
            case MediaPlaybackState.None: return MediaPlayerState.Stopped;
            case MediaPlaybackState.Opening: return MediaPlayerState.Loading;
            case MediaPlaybackState.Paused: return MediaPlayerState.Paused;
            case MediaPlaybackState.Playing: return MediaPlayerState.Playing;
         };

         return MediaPlayerState.Paused;
      }

      public override TimeSpan Position => _player.PlaybackSession.Position;

      public override TimeSpan Duration => _player.PlaybackSession.NaturalDuration;

      // - - -  - - - 

      //public new bool play(string uri)
      //{
      //   _player.Source = MediaSource.CreateFromUri(new Uri(uri));
      //   _player.Play();
      //   return true;
      //}

      public override async Task<IMediaItem> Play(string uri)
      {
         _player.Source = MediaSource.CreateFromUri(new Uri(uri));
         _player.Play();
         return null;

         var mediaItem = await MediaExtractor.CreateMediaItem(uri);

         var mediaPlaybackList = new MediaPlaybackList();
         var mediaSource = await CreateMediaSource(mediaItem);
         var item = new MediaPlaybackItem(mediaSource);
         mediaPlaybackList.Items.Add(item);
         _player.Source = mediaPlaybackList;
         _player.Play();

         return mediaItem;
      }

      private async Task<MediaSource> CreateMediaSource(IMediaItem mediaItem)
      {
         switch (mediaItem.MediaLocation)
         {
            case MediaLocation.Remote:
               return MediaSource.CreateFromUri(new Uri(mediaItem.MediaUri));

            case MediaLocation.FileSystem:
               var du = _player.SystemMediaTransportControls.DisplayUpdater;
               var storageFile = await StorageFile.GetFileFromPathAsync(mediaItem.MediaUri);
               var playbackType = (mediaItem.MediaType == MediaType.Audio ? Windows.Media.MediaPlaybackType.Music : Windows.Media.MediaPlaybackType.Video);
               await du.CopyFromFileAsync(playbackType, storageFile);
               du.Update();
               return MediaSource.CreateFromStorageFile(storageFile);
         }

         return MediaSource.CreateFromUri(new Uri(mediaItem.MediaUri));
      }

      public override Task Stop()
      {
         _player.PlaybackSession.PlaybackRate = 0;
         _player.PlaybackSession.Position = TimeSpan.Zero;

         _State = MediaPlayerState.Stopped;
         //this.OnStateChanged(this, new StateChangedEventArgs(_State));

         return Task.CompletedTask;
      }

      public override async Task SeekTo(TimeSpan position)
      {
         _player.PlaybackSession.Position = position;
         await Task.CompletedTask;
      }
   }
}
