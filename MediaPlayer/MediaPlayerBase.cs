using System;
using System.Threading.Tasks;
using System.Timers;

namespace ZPF.Media
{
   public abstract class MediaPlayerBase : IMediaPlayer
   {
      public abstract object Player { get; }
      public abstract IMediaExtractor MediaExtractor { get; set; }

      public IPlaylist Playlist { get; set; } = new Playlist();

      public Timer Timer { get; } = new Timer(1000);

      public MediaPlayerBase()
      {
         Timer.AutoReset = true;
         Timer.Elapsed += Timer_Elapsed;
         Timer.Start();
      }

      private TimeSpan PreviousPosition = new TimeSpan();
      protected virtual void Timer_Elapsed(object sender, ElapsedEventArgs e)
      {
         if (!IsInitialized)
            return;

         if (PreviousPosition != Position)
         {
            PreviousPosition = Position;
            OnPositionChanged(this, new PositionChangedEventArgs(Position, Duration));
         }

         //if (this.IsBuffering())
         //{
         //   OnBufferingChanged(this, new BufferingChangedEventArgs(Buffered));
         //}
      }

      public TimeSpan StepSize { get; set; } = TimeSpan.FromSeconds(10);

      //ToDo: set private
      public bool IsInitialized { get; set; }

      public abstract MediaPlayerState State { get; }
      public abstract TimeSpan Position { get; }
      public abstract TimeSpan Duration { get; }
      public abstract TimeSpan Buffered { get; }

      public abstract decimal Volume { get; set; }
      public abstract decimal Balance { get; set; }
      public abstract bool Muted { get; set; }

      public abstract void Init(object mainWindow = null);

      public abstract Task<IMediaItem> Play(string uri);

      public abstract Task Play(IMediaItem mediaItem);

      public abstract Task Play();

      public abstract Task Pause();

      public abstract Task Stop();

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public abstract Task SeekTo(TimeSpan position);

      public virtual Task StepBackward()
      {
         var seekTo = this.SeekTo(TimeSpan.FromSeconds(Double.IsNaN(Position.TotalSeconds) ? 0 : ((Position.TotalSeconds < StepSize.TotalSeconds) ? 0 : Position.TotalSeconds - StepSize.TotalSeconds)));
         Timer_Elapsed(null, null);
         return seekTo;
      }

      public virtual Task StepForward()
      {
         var seekTo = this.SeekTo(TimeSpan.FromSeconds(Double.IsNaN(Position.TotalSeconds) ? 0 : Position.TotalSeconds + StepSize.TotalSeconds));
         Timer_Elapsed(null, null);
         return seekTo;
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public event StateChangedEventHandler StateChanged;
      public event BufferingChangedEventHandler BufferingChanged;
      public event PositionChangedEventHandler PositionChanged;

      public event MediaItemFinishedEventHandler MediaItemFinished;
      public event MediaItemChangedEventHandler MediaItemChanged;
      public event MediaItemFailedEventHandler MediaItemFailed;

      public void OnStateChanged(object sender, StateChangedEventArgs e) => StateChanged?.Invoke(sender, e);
      public void OnBufferingChanged(object sender, BufferingChangedEventArgs e) => BufferingChanged?.Invoke(sender, e);
      public void OnPositionChanged(object sender, PositionChangedEventArgs e) => PositionChanged?.Invoke(sender, e);

      public void OnMediaItemChanged(object sender, MediaItemEventArgs e) => MediaItemChanged?.Invoke(sender, e);
      public void OnMediaItemFailed(object sender, MediaItemFailedEventArgs e) => MediaItemFailed?.Invoke(sender, e);
      public void OnMediaItemFinished(object sender, MediaItemEventArgs e) => MediaItemFinished?.Invoke(sender, e);

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -


   }
}


