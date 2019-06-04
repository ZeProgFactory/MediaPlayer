using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ZPF.Media
{
   /// <summary>
   /// MediaPlayerImplementation for WPF ...
   /// </summary>
   public class MediaPlayerImplementation : MediaPlayerBase
   {
      Window _Window = null;

      public override object Player { get => _player; }

      private readonly System.Windows.Controls.MediaElement _player;

      public override IMediaExtractor MediaExtractor { get => _MediaExtractor; set => _MediaExtractor = value; }
      private IMediaExtractor _MediaExtractor;

      public MediaPlayerImplementation()
      {
         _player = new System.Windows.Controls.MediaElement();
         _player.LoadedBehavior = MediaState.Play;
         _player.UnloadedBehavior = MediaState.Manual;
         _player.Volume = 1;
         _player.IsMuted = false;

         _MediaExtractor = new MediaExtractor();


         _player.MediaEnded += async (object sender, System.Windows.RoutedEventArgs e) =>
         {
            if (this.Playlist.HasNext())
            {
               await this.Playlist.PlayNext();
            };

            this.OnMediaItemFinished(this, new MediaItemEventArgs(this.Playlist.Current));
         };

         _player.MediaOpened += (object sender, System.Windows.RoutedEventArgs e) =>
         {
            _playerPlay();
         };

         _player.BufferingStarted += (object sender, System.Windows.RoutedEventArgs e) =>
         {
            SetState(MediaPlayerState.Buffering);
         };

         _player.BufferingEnded += (object sender, System.Windows.RoutedEventArgs e) =>
         {
            _playerPlay();
            SetState(MediaPlayerState.Playing);
         };

         _player.MediaFailed += (object sender, System.Windows.ExceptionRoutedEventArgs e) =>
         {
            _State = MediaPlayerState.Failed;
            _player.Position = TimeSpan.Zero;
            this.OnMediaItemFailed(this, new MediaItemFailedEventArgs(this.Playlist.Current, e.ErrorException, e.ErrorException.Message));
         };
      }

      // - - -  - - - 

      public override void Init(object mainWindow = null)
      {
         _Window = (Window)mainWindow;
         IsInitialized = (_Window != null);
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public override MediaPlayerState State
      {
         get { return GetMediaPlayerState(); }
      }
      private MediaPlayerState _State;

      private void SetState(MediaPlayerState state)
      {
         _State = state;
         this.OnStateChanged(this, new StateChangedEventArgs(_State));
      }

      // - - -  - - - 

      private MediaPlayerState GetMediaPlayerState()
      {
         // Playing, Paused, Stopped, Loading, Buffering, Failed

         if (_player.IsBuffering) return MediaPlayerState.Buffering;

         return _State;
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public override TimeSpan Position
      {
         get
         {
            TimeSpan ts = TimeSpan.MinValue;

            _Window.Dispatcher.Invoke(() =>
            {
               ts = _player.Position;
            });

            return ts;
         }
      }

      public override TimeSpan Duration
      {
         get
         {
            TimeSpan ts = TimeSpan.MinValue;

            _Window.Dispatcher.Invoke(() =>
            {
               ts = (_player.NaturalDuration.HasTimeSpan ? _player.NaturalDuration.TimeSpan : TimeSpan.MaxValue);
            });

            return ts;
         }
      }

      public override TimeSpan Buffered
      {
         get
         {
            TimeSpan ts = TimeSpan.MinValue;

            _Window.Dispatcher.Invoke(() =>
            {
               ts = (_player.NaturalDuration.HasTimeSpan ? TimeSpan.FromSeconds(_player.NaturalDuration.TimeSpan.TotalSeconds * _player.BufferingProgress) : TimeSpan.MinValue);
            });

            return ts;
         }
      }

      // - - -  - - - 

      public override decimal Volume
      {
         get
         {
            return (decimal)_player.Volume;
         }
         set
         {
            _player.Volume = (double)value;
         }
      }

      //ToDo: HasBalance --> Yes
      public override decimal Balance
      {
         get
         {
            return (decimal)_player.Balance;
         }
         set
         {
            _player.Balance = (double)value;
         }
      }

      public override bool Muted
      {
         get
         {
            return _player.IsMuted;
         }
         set
         {
            _player.IsMuted = value;
         }
      }

      // - - -  - - - 

      public override Task Pause()
      {
         _player.Pause();
         SetState(MediaPlayerState.Paused);

         return Task.CompletedTask;
      }

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

         if (Playlist.Current?.MediaUri != mediaItem.MediaUri)
         {
            Playlist.Current = mediaItem;
         };
      }

      public override async Task SetSource(IMediaItem mediaItem)
      {
         if (!mediaItem.IsMetadataExtracted)
         {
            mediaItem = await MediaExtractor.CreateMediaItem(mediaItem);
         };

         _player.Source = new Uri(mediaItem.MediaUri);
      }

      public override Task Play()
      {
         _playerPlay();
         SetState(MediaPlayerState.Playing);

         return Task.CompletedTask;
      }

      private void _playerPlay()
      {
         try
         {
            _player.Play();
         }
         catch (Exception ex)
         {
            _State = MediaPlayerState.Failed;
            // _player.Position = TimeSpan.Zero;
            this.OnMediaItemFailed(this, new MediaItemFailedEventArgs(this.Playlist.Current, ex, ex.Message));
         };
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - - 

      public override Task SeekTo(TimeSpan position)
      {
         _player.Position = position;
         Play();

         return Task.CompletedTask;
      }

      public override Task Stop()
      {
         _player.Stop();
         SetState(MediaPlayerState.Stopped);

         return Task.CompletedTask;
      }
   }
}
