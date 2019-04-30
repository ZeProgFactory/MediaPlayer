using System;
using System.Threading.Tasks;
using System.Timers;

namespace ZPF.Media
{
   public enum MediaPlayerState
   {
      Playing,
      Paused,
      Stopped,
      Loading,
      Buffering,
      Failed
   }

   public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs e);
   public delegate void PlayingChangedEventHandler(object sender, PlayingChangedEventArgs e);
   public delegate void BufferingChangedEventHandler(object sender, BufferingChangedEventArgs e);
   public delegate void PositionChangedEventHandler(object sender, PositionChangedEventArgs e);
   public delegate void MediaItemFinishedEventHandler(object sender, MediaItemEventArgs e);
   public delegate void MediaItemChangedEventHandler(object sender, MediaItemEventArgs e);
   public delegate void MediaItemFailedEventHandler(object sender, MediaItemFailedEventArgs e);

   public interface IMediaPlayer
   {
      void Init();

      /// <summary>
      /// Reading the current status of the player
      /// </summary>
      MediaPlayerState State { get; }
      TimeSpan Position { get; }
      TimeSpan Duration { get; }
      TimeSpan Buffered { get; }

      TimeSpan StepSize { get; set; }

      bool IsInitialized { get; set; }

      IMediaExtractor MediaExtractor { get; set; }
      IVolumeManager VolumeManager { get; set; }


      /// <summary>
      /// Plays an uri (remote or local)
      /// </summary>
      /// <param name="uri"></param>
      /// <returns></returns>
      Task<IMediaItem> Play(string uri);

      /// <summary>
      /// Starts playing
      /// </summary>
      Task Play();

      /// <summary>
      /// Stops playing but retains position
      /// </summary>
      Task Pause();

      /// <summary>
      /// Stops playing
      /// </summary>
      Task Stop();

      /// <summary>
      /// Changes position to the specified number of milliseconds from zero
      /// </summary>
      Task SeekTo(TimeSpan position);

      /// <summary>
      /// Seeks forward a fixed amount of seconds of the current MediaFile
      /// </summary>
      Task StepForward();

      /// <summary>
      /// Seeks backward a fixed amount of seconds of the current MediaFile
      /// </summary>
      Task StepBackward();

      // - - -   - - - 

      event StateChangedEventHandler StateChanged;

      event PlayingChangedEventHandler PlayingChanged;

      event BufferingChangedEventHandler BufferingChanged;

      event PositionChangedEventHandler PositionChanged;

      event MediaItemFinishedEventHandler MediaItemFinished;

      event MediaItemChangedEventHandler MediaItemChanged;

      event MediaItemFailedEventHandler MediaItemFailed;
   }

   public abstract class MediaPlayerBase : IMediaPlayer
   {
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
            OnPositionChanged(this, new PositionChangedEventArgs(Position));
         }
         if (this.IsPlaying())
         {
            OnPlayingChanged(this, new PlayingChangedEventArgs(Position, Duration));
         }
         if (this.IsBuffering())
         {
            OnBufferingChanged(this, new BufferingChangedEventArgs(Buffered));
         }
      }

      public TimeSpan StepSize { get; set; } = TimeSpan.FromSeconds(10);

      //ToDo: set private
      public bool IsInitialized { get; set; }

      public IMediaExtractor MediaExtractor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public IVolumeManager VolumeManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

      public abstract MediaPlayerState State { get; }
      public abstract TimeSpan Position { get; }
      public abstract TimeSpan Duration { get; }
      public abstract TimeSpan Buffered { get; }

      public abstract void Init();

      public abstract Task<IMediaItem> Play(string uri);

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
      public event PlayingChangedEventHandler PlayingChanged;
      public event BufferingChangedEventHandler BufferingChanged;
      public event PositionChangedEventHandler PositionChanged;

      public event MediaItemFinishedEventHandler MediaItemFinished;
      public event MediaItemChangedEventHandler MediaItemChanged;
      public event MediaItemFailedEventHandler MediaItemFailed;

      public void OnStateChanged(object sender, StateChangedEventArgs e) => StateChanged?.Invoke(sender, e);
      public void OnPlayingChanged(object sender, PlayingChangedEventArgs e) => PlayingChanged?.Invoke(sender, e);
      public void OnBufferingChanged(object sender, BufferingChangedEventArgs e) => BufferingChanged?.Invoke(sender, e);
      public void OnPositionChanged(object sender, PositionChangedEventArgs e) => PositionChanged?.Invoke(sender, e);

      public void OnMediaItemChanged(object sender, MediaItemEventArgs e) => MediaItemChanged?.Invoke(sender, e);
      public void OnMediaItemFailed(object sender, MediaItemFailedEventArgs e) => MediaItemFailed?.Invoke(sender, e);
      public void OnMediaItemFinished(object sender, MediaItemEventArgs e) => MediaItemFinished?.Invoke(sender, e);

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -


   }
}


