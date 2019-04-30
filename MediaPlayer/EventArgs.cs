using System;

namespace ZPF.Media
{
   public class StateChangedEventArgs : EventArgs
   {
      public StateChangedEventArgs(MediaPlayerState state)
      {
         State = state;
      }

      public MediaPlayerState State { get; }
   }

   public class PlayingChangedEventArgs : EventArgs
   {
      public PlayingChangedEventArgs(TimeSpan position, TimeSpan duration)
      {
         Position = position;
         Duration = duration;
      }
      public TimeSpan Position { get; }
      public TimeSpan Duration { get; }
   }

   public class PositionChangedEventArgs : EventArgs
   {
      public PositionChangedEventArgs(TimeSpan position)
      {
         Position = position;
      }

      public TimeSpan Position { get; }
   }

   public class BufferingChangedEventArgs : EventArgs
   {
      public BufferingChangedEventArgs(TimeSpan buffered)
      {
         Buffered = buffered;
      }

      public TimeSpan Buffered { get; }
   }

   public class MediaItemEventArgs : EventArgs
   {
      public MediaItemEventArgs(IMediaItem mediaItem)
      {
         MediaItem = mediaItem;
      }

      public IMediaItem MediaItem { get; private set; }
   }

   public class MediaItemFailedEventArgs : MediaItemEventArgs
   {
      public MediaItemFailedEventArgs(IMediaItem Item, Exception Exception, string Message) : base(Item)
      {
         this.Exeption = Exception;
         this.Message = Message;
      }

      public Exception Exeption { get; private set; }
      public string Message { get; private set; }
   }
}
