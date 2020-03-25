using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.App;

/// <summary>
/// https://developer.android.com/reference/android/media/MediaPlayer.html
/// 
/// https://docs.microsoft.com/en-us/xamarin/android/app-fundamentals/android-audio
/// https://github.com/jamesmontemagno/AndroidStreamingAudio
/// 
/// https://github.com/xamarin/docs-archive/tree/master/Recipes/android/media/audio/play_audio
/// https://forums.xamarin.com/discussion/22085/playing-audio-files-in-xamarin-forms
/// https://github.com/tkowalczyk/SimpleAudioForms
/// https://stackoverflow.com/questions/33086417/mediaplayer-setdatasourcestring-not-working-with-local-files
/// https://devblogs.microsoft.com/xamarin/background-audio-streaming-with-xamarin-android/
/// </summary>

namespace ZPF.Media
{
   public class MediaPlayerImplementation : MediaPlayerBase
   {
      public override object Player { get => _player; }
      private readonly Android.Media.MediaPlayer _player;
      //private readonly AudioTrack _player;

      public override IMediaExtractor MediaExtractor { get => _MediaExtractor; set => _MediaExtractor = value; }
      private IMediaExtractor _MediaExtractor;

      public MediaPlayerImplementation()
      {
         _player = new Android.Media.MediaPlayer();

         _player.Error += (sender, args) =>
         {
            //playback error
            Console.WriteLine("Error in playback resetting: " + args.What);
            _player.Stop();//this will clean up and reset properly.
         };

         _player.Info += (object sender, Android.Media.MediaPlayer.InfoEventArgs e) =>
         {
            Debug.WriteLine($"*** Info {e.What}");
         };

         _player.Prepared += (s, e) =>
         {
            _player.Start();
            SetState(MediaPlayerState.Playing);

            this.OnMediaItemChanged(this, new MediaItemEventArgs(this.Playlist.Current));
         };

         _player.BufferingUpdate += (s, e) =>
         {
            // in %
            Debug.WriteLine($"*** BufferingUpdate {e.Percent}");
         };

         _player.Completion += async (s, e) =>
         {
            Debug.WriteLine($"*** Completion {e.ToString()}");

            if (this.Playlist.HasNext())
            {
               await this.Playlist.PlayNext();
            };

            this.OnMediaItemFinished(this, new MediaItemEventArgs(this.Playlist.Current));
         };

         _player.TimedMetaDataAvailable += (s, e) =>
         {
            Debug.WriteLine($"*** TimedMetaDataAvailable {e.Data.ToString()}");
         };

         _player.TimedText += (s, e) =>
         {
            Debug.WriteLine($"*** TimedText {e.Text.Text }");
         };

         _MediaExtractor = new ZPF.Media.MediaExtractor();

         IsInitialized = true;
      }

      public override void Init(object mainWindow = null)
      {
         IsInitialized = true;
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

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

      public override TimeSpan Position => TimeSpan.FromMilliseconds(_player.CurrentPosition);

      public override TimeSpan Duration => TimeSpan.FromMilliseconds(_player.Duration);

      public override TimeSpan Buffered => throw new NotImplementedException();

      // - - -  - - - 

      float _Volume = 1;
      float _Balance = 1;
      bool _Muted = false;

      public override decimal Volume { get => (decimal)_Volume; set { _Volume = (float)value; SetVolume(); } }
      public override decimal Balance { get => (decimal)_Balance; set { _Balance = (float)value; SetVolume(); } }
      public override bool Muted { get => _Muted; set { _Muted = value; SetVolume(); } }

      void SetVolume()
      {
         if (_Muted)
         {
            _player.SetVolume(0, 0);
            return;
         };

         float _LeftVolume = _Volume;
         float _RightVolume = _Volume;

         if (Balance == 0)
         {
            // nothing 
         }
         else if (Balance == 0)
         {
            // left
            _RightVolume *= (float)(1 + Balance);
         }
         else
         {
            // right
            _LeftVolume *= (float)(1 - Balance);
         };

         _player.SetVolume(_LeftVolume, _RightVolume);
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

         //await SetSource(mediaItem);
      }

      public override async Task SetSource(IMediaItem mediaItem)
      {
         if (!mediaItem.IsMetadataExtracted)
         {
            mediaItem = await MediaExtractor.CreateMediaItem(mediaItem);
         };

         //Playlist.Current = mediaItem;

         //if (_player.IsPlaying)
         {
            _player.Reset();
         };

         _player.SetDataSource(mediaItem.MediaUri);
         _player.Prepare();
      }

      public override Task Play()
      {
         _player.Start();

         return Task.CompletedTask;
      }

      public override Task SeekTo(TimeSpan position)
      {
         _player.SeekTo(position.Milliseconds);

         return Task.CompletedTask;
      }

      public override Task Stop()
      {
         _player.Stop();

         return Task.CompletedTask;
      }
   }
}
