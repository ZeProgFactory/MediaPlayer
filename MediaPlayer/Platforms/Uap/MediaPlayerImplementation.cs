using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using ZPF.Media.Uap;

namespace ZPF.Media
{
   public class MediaPlayerImplementation : MediaPlayerBase
   {
      private readonly Windows.Media.Playback.MediaPlayer _player;

      public override IMediaExtractor MediaExtractor { get => _MediaExtractor; set => _MediaExtractor = value; }
      private IMediaExtractor _MediaExtractor;

      public MediaPlayerImplementation()
      {
         _player = new Windows.Media.Playback.MediaPlayer();
         _MediaExtractor = new MediaExtractor();

         _player.MediaEnded += async (Windows.Media.Playback.MediaPlayer sender, object args) =>
         {
            if (this.Playlist.HasNext())
            {
               await this.Playlist.PlayNext();
            };

            this.OnMediaItemFinished(this, new MediaItemEventArgs(this.Playlist.Current));
         };

         _player.CurrentStateChanged += (Windows.Media.Playback.MediaPlayer sender, object args) =>
         {
            _State = GetMediaPlayerState();
            this.OnStateChanged(this, new StateChangedEventArgs(GetMediaPlayerState()));
         };

         _player.SourceChanged += (Windows.Media.Playback.MediaPlayer sender, object args) =>
         {
            this.OnMediaItemChanged(this, new MediaItemEventArgs(this.Playlist.Current));
         };

         _player.MediaFailed += (Windows.Media.Playback.MediaPlayer sender, MediaPlayerFailedEventArgs args) =>
         {
            _State = MediaPlayerState.Failed;
            _player.PlaybackSession.Position = TimeSpan.Zero;
            this.OnMediaItemFailed(this, new MediaItemFailedEventArgs(this.Playlist.Current, args.ExtendedErrorCode, args.ErrorMessage));
         };
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public override void Init()
      {
         IsInitialized = true;
      }

      // - - -  - - - 

      public override MediaPlayerState State
      {
         get { return _State; }
         //ToDo: ME discuss with CHM
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

      public override TimeSpan Buffered
      {
         get
         {
            //ToDo: ME - ???
            if (_player == null) return TimeSpan.Zero;
            return
                TimeSpan.FromMilliseconds(_player.PlaybackSession.BufferingProgress *
                                          _player.PlaybackSession.NaturalDuration.TotalMilliseconds);
         }
      }

      public override decimal CurrentVolume { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public override bool Muted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

      // - - -  - - - 

      //public new bool play(string uri)
      //{
      //   _player.Source = MediaSource.CreateFromUri(new Uri(uri));
      //   _player.Play();
      //   return true;
      //}

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
            //ToDo: ...
         };

         var mediaPlaybackList = new MediaPlaybackList();
         var mediaSource = await CreateMediaSource(mediaItem);
         var item = new MediaPlaybackItem(mediaSource);
         mediaPlaybackList.Items.Add(item);

         //Playlist.Clear();
         Playlist.Current = mediaItem;

         _player.Source = mediaPlaybackList;
         _player.Play();
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

      public override Task Play()
      {
         _player.PlaybackSession.PlaybackRate = (_player.PlaybackSession.PlaybackRate == 0 ? _player.PlaybackSession.PlaybackRate = 1 : _player.PlaybackSession.PlaybackRate);
         _player.Play();
         return Task.CompletedTask;
      }

      public override Task Pause()
      {
         if (_player.PlaybackSession.PlaybackState == MediaPlaybackState.Paused)
         {
            _player.Play();
         }
         else
         {
            _player.Pause();
         };

         return Task.CompletedTask;
      }

      public override Task Stop()
      {
         _player.PlaybackSession.PlaybackRate = 0;
         _player.PlaybackSession.Position = TimeSpan.Zero;

         //ToDO: ??? SetState(MediaPlayerState.Stopped);
         _State = MediaPlayerState.Stopped;
         this.OnStateChanged(this, new StateChangedEventArgs(_State));

         return Task.CompletedTask;
      }

      public override async Task SeekTo(TimeSpan position)
      {
         _player.PlaybackSession.Position = position;
         await Task.CompletedTask;
      }
   }
}
