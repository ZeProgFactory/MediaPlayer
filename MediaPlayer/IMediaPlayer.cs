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
      object Player { get; }

      IMediaExtractor MediaExtractor { get; set; }
      IPlaylist Playlist { get; set; }

      // - - -  - - - 

      void Init();
      bool IsInitialized { get; set; }

      /// <summary>
      /// Reading the current status of the player
      /// </summary>
      MediaPlayerState State { get; }

      /// <summary>
      /// Gets the players position
      /// </summary>
      TimeSpan Position { get; }

      /// <summary>
      /// Gets the source duration
      /// If the response is TimeSpan.Zero, the duration is unknown or the player is still buffering.
      /// </summary>
      TimeSpan Duration { get; }

      /// <summary>
      /// Gets the buffered time
      /// </summary>
      TimeSpan Buffered { get; }

      /// <summary>
      /// Managing the step size for the step forward and step backward functions
      /// </summary>
      TimeSpan StepSize { get; set; }

      /// <summary>
      /// Plays an uri (remote or local)
      /// </summary>
      /// <param name="uri"></param>
      /// <returns></returns>
      Task<IMediaItem> Play(string uri);

      /// <summary>
      /// Plays a MediaItem
      /// </summary>
      /// <param name="uri"></param>
      /// <returns></returns>
      Task Play(IMediaItem mediaItem);

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
      /// Seeks forward a fixed amount of seconds of the current MediaItem
      /// </summary>
      Task StepForward();

      /// <summary>
      /// Seeks backward a fixed amount of seconds of the current MediaItem
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


      // - - -
      /// <summary>
      /// Get or sets the audio volume for media playback.
      /// The range is between 0 and 1. The default value is 1.
      /// </summary>
      decimal Volume { get; set; }

      /// <summary>
      /// Gets or sets a ratio of volume across stereo speakers.
      /// The ratio of volume across speakers in the range between -1 and 1. The default value is 0.
      /// </summary>
      decimal Balance { get; set; }

      /// <summary>
      /// Gets or sets a Boolean value indicating if the audio is muted.
      /// </summary>
      bool Muted { get; set; }
   }
}


