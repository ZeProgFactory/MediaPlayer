using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZPF.Media
{
   public class MediaPlayerImplementation : MediaPlayerBase
   {
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
            MediaElement _Sound = sender as MediaElement;
            _Sound.Play();
         };

         _player.BufferingStarted += (object sender, System.Windows.RoutedEventArgs e) =>
         {
            SetState(MediaPlayerState.Buffering);
         };

         _player.BufferingEnded += (object sender, System.Windows.RoutedEventArgs e) =>
         {
            _player.Play();
            SetState(MediaPlayerState.Playing);
         };

         _player.MediaFailed += (object sender, System.Windows.ExceptionRoutedEventArgs e) =>
         {
            _State = MediaPlayerState.Failed;
            _player.Position = TimeSpan.Zero;
            this.OnMediaItemFailed(this, new MediaItemFailedEventArgs(this.Playlist.Current, e.ErrorException, e.ErrorException.Message));
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
      }
      private MediaPlayerState _State;

      private void SetState(MediaPlayerState state)
      {
         _State = state;
         this.OnStateChanged(this, new StateChangedEventArgs(_State));
      }

      // - - -  - - - 

      public override TimeSpan Position => _player.Position;

      public override TimeSpan Duration => (_player.NaturalDuration.HasTimeSpan ? _player.NaturalDuration.TimeSpan : TimeSpan.MaxValue);

      public override TimeSpan Buffered => throw new NotImplementedException();

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

      //ToDo: HasBalance
      public override decimal Balance
      {
         get
         {
            return (decimal)0;
         }
         set
         {
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
         return Task.CompletedTask;
      }

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

         _player.Source = new Uri(mediaItem.MediaUri);
         _player.Play();
      }

      public override Task Play()
      {
         _player.Pause();
         return Task.CompletedTask;
      }

      public override Task SeekTo(TimeSpan position)
      {
         throw new NotImplementedException();
      }

      public override Task Stop()
      {
         throw new NotImplementedException();
      }
   }
}
