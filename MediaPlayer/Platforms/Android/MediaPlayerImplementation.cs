using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Android;
using Android.Media;

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
         };

         _player.BufferingUpdate += (s, e) =>
         {
            // in %
            Debug.WriteLine($"*** BufferingUpdate {e.Percent}");
         };

         _player.Completion += (s, e) =>
         {
            Debug.WriteLine($"*** Completion {e.ToString()}");
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
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public override MediaPlayerState State => throw new System.NotImplementedException();

      public override TimeSpan Position => TimeSpan.FromMilliseconds(_player.CurrentPosition);

      public override TimeSpan Duration => TimeSpan.FromMilliseconds(_player.Duration);

      public override TimeSpan Buffered => throw new NotImplementedException();

      public override decimal CurrentVolume { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public override bool Muted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

      public override void Init()
      {
         IsInitialized = true;
      }

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

      public override Task Play()
      {
         _player.Start();

         return Task.CompletedTask;
      }

      public override Task Play(IMediaItem mediaItem)
      {
         if (_player.IsPlaying)
         {
            _player.Reset();
         };

         _player.SetDataSource(mediaItem.MediaUri);
         _player.Prepare();

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
