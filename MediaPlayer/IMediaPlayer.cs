using System;
using System.Threading.Tasks;
using System.Timers;

namespace ZPF.Media
{
   public interface IMediaPlayer
   {
      void Init();

      TimeSpan StepSize { get; set; }

      bool IsInitialized { get; set; }

      IMediaExtractor MediaExtractor { get; set; }
      IVolumeManager VolumeManager { get; set; }

      Task<IMediaItem> Play(string uri);
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

         //if (PreviousPosition != Position)
         //{
         //   PreviousPosition = Position;
         //   OnPositionChanged(this, new PositionChangedEventArgs(Position));
         //}
         //if (this.IsPlaying())
         //{
         //   OnPlayingChanged(this, new PlayingChangedEventArgs(Position, Duration));
         //}
         //if (this.IsBuffering())
         //{
         //   OnBufferingChanged(this, new BufferingChangedEventArgs(Buffered));
         //}
      }

      public TimeSpan StepSize { get; set; } = TimeSpan.FromSeconds(10);

      //ToDo: set private
      public bool IsInitialized { get; set; }

      public IMediaExtractor MediaExtractor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public IVolumeManager VolumeManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

      public abstract void Init();

      public abstract Task<IMediaItem> Play(string uri);

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -
   }
}


