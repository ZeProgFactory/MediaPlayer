using System;
using System.Threading.Tasks;
using Android;
using Android.Media;

/// <summary>
/// https://github.com/xamarin/docs-archive/tree/master/Recipes/android/media/audio/play_audio
/// https://forums.xamarin.com/discussion/22085/playing-audio-files-in-xamarin-forms
/// https://github.com/tkowalczyk/SimpleAudioForms
/// https://stackoverflow.com/questions/33086417/mediaplayer-setdatasourcestring-not-working-with-local-files
/// </summary>

namespace ZPF.Media
{
   public class MediaPlayerImplementation : MediaPlayerBase
   {
      private readonly Android.Media.MediaPlayer _player;

      public override IMediaExtractor MediaExtractor { get => _MediaExtractor; set => _MediaExtractor = value; }
      private IMediaExtractor _MediaExtractor;

      public MediaPlayerImplementation()
      {
         _player = new Android.Media.MediaPlayer();
         // _player = Android.Media.MediaPlayer.Create(global::Android.App.Application.Context, null);

         _MediaExtractor = new ZPF.Media.MediaExtractor();
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -


      public override MediaPlayerState State => throw new System.NotImplementedException();

      public override TimeSpan Position => throw new NotImplementedException();

      public override TimeSpan Duration => throw new NotImplementedException();

      public override TimeSpan Buffered => throw new NotImplementedException();

      public override decimal CurrentVolume { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public override bool Muted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

      public override void Init()
      {
         IsInitialized = true;
      }

      public override Task Pause()
      {
         throw new NotImplementedException();
      }

      public override async Task<IMediaItem> Play(string uri)
      {
         var mediaItem = await MediaExtractor.CreateMediaItem(uri);

         _player.Prepared += (s, e) =>
         {
            _player.Start();
         };

         try
         {
            _player.SetDataSource(uri);
            _player.Prepare();
         }
         catch
         {
         };

         return mediaItem;
      }

      public override Task Play()
      {
         throw new NotImplementedException();
      }

      public override Task Play(IMediaItem mediaItem)
      {
         throw new NotImplementedException();
      }

      public override Task SeekTo(TimeSpan position)
      {
         throw new NotImplementedException();
      }

      public override Task Stop()
      {
         throw new System.NotImplementedException();
      }
   }
}
