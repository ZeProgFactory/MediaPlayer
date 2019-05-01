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
      IMediaExtractor MediaExtractor { get; set; }
      IVolumeManager VolumeManager { get; set; }
      IPlaylist Playlist { get; set; }

      // - - -  - - - 

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
   }
}


