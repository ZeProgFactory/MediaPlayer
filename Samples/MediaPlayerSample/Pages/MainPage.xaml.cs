using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using ZPF.Media;
using ZPF.XF;
using ZPFMediaPlayer;

namespace MediaPlayerSample
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(true)]
   public partial class MainPage : ContentPage
   {
      public MainPage()
      {
         InitializeComponent();

         // - - -  - - - 

         MediaPlayer.Current.Playlist.RepeatMode = RepeatMode.Off; ;
         MediaPlayer.Current.Playlist.ShuffleMode = ShuffleMode.Off;

         // Hook into events
         MediaPlayer.Current.StateChanged += Current_StateChanged;
         MediaPlayer.Current.PositionChanged += Current_PositionChanged;

         MediaPlayer.Current.BufferingChanged += Current_BufferingChanged;

         MediaPlayer.Current.MediaItemFinished += Current_MediaItemFinished;
         MediaPlayer.Current.MediaItemChanged += Current_MediaItemChanged;


         listViewPlaylist.BindingContext = MediaPlayer.Current;
         listViewPlaylist.ItemsSource = MediaPlayer.Current.Playlist;

         listViewPlaylist.SetBinding(ListView.SelectedItemProperty, new Binding("Current", BindingMode.TwoWay, source: MediaPlayer.Current.Playlist));

         // - - -  - - - 

         #region FontIcon stuff

         switch (Device.RuntimePlatform)
         {
            case Device.Android:
            case Device.UWP:  
               btnPlayPrevious.Text = "";
               btnPlayPrevious.ImageSource = SkiaHelper.SkiaFontIcon(ZPFFonts.MPF.Media_Backward, 64);

               btnStepBackward.Text = "";
               btnStepBackward.ImageSource = SkiaHelper.SkiaFontIcon(ZPFFonts.MPF.Media_Previous, 64);

               btnPlayPause.Text = "";
               btnPlayPause.ImageSource = SkiaHelper.SkiaFontIcon(ZPFFonts.MPF.Media_Play_01, 64);

               btnStepForward.Text = "";
               btnStepForward.ImageSource = SkiaHelper.SkiaFontIcon(ZPFFonts.MPF.Media_Next, 64);

               btnPlayNext.Text = "";
               btnPlayNext.ImageSource = SkiaHelper.SkiaFontIcon(ZPFFonts.MPF.Media_Fast_Forward, 64);

               btnStop.Text = "";
               btnStop.ImageSource = SkiaHelper.SkiaFontIcon(ZPFFonts.MPF.Media_Stop, 64);
               break;

            case Device.WPF:
            default:
               break;
         };

         #endregion
      }

      private void Current_BufferingChanged(object sender, BufferingChangedEventArgs e)
      {
         throw new NotImplementedException();
      }

      private void Current_MediaItemFinished(object sender, MediaItemEventArgs e)
      {
         //ToDo: ??? event args: Old song or new song ???
         Debug.WriteLine("Current_MediaItemFinished");
      }

      private void Current_MediaItemChanged(object sender, MediaItemEventArgs e)
      {
         Debug.WriteLine("Current_MediaItemChanged");
      }

      private void Current_PositionChanged(object sender, PositionChangedEventArgs e)
      {
         Device.BeginInvokeOnMainThread(() =>
         {
            labelPos.Text = $"{MediaPlayer.Current.Position.ToDisplay()}";
         });
      }

      private void Current_StateChanged(object sender, StateChangedEventArgs e)
      {
         Device.BeginInvokeOnMainThread(() =>
         {
            labelInfo.Text = $"{e.State} {(MediaPlayer.Current.Duration == TimeSpan.MaxValue ? "" : MediaPlayer.Current.Duration.ToDisplay())} ";
         });
      }

      private async void Button_Audio_Clicked(object sender, EventArgs e)
      {
         //Audio
         //await MediaPlayer.Current.Play("https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");

         await MediaPlayer.Current.Play("http://freesound.org/data/previews/273/273629_4068345-lq.mp3");
      }

      private async void Button_MediaItem_Clicked(object sender, EventArgs e)
      {
         //Audio
         var mi = new MediaItem("https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3")
         {
            MediaLocation = MediaLocation.Remote,
            MediaType = MediaType.Audio,
            //ToDo: MediaType = MediaType.SmoothStreaming,
         };

         try
         {
            await MediaPlayer.Current.Play(mi);
         }
         catch(Exception ex)
         {
            Debug.WriteLine(ex.Message);

            if (Device.RuntimePlatform == Device.WPF)
            {
               await DisplayAlert("Oups ...", "You stumbled into a known error ( https://github.com/ZeProgFactory/MediaPlayer/issues/3 ) ", "ok");
            }
            else
            {
               await DisplayAlert("Oups ...", "This should not happen. Please report the problem ...", "ok");
            };
         };
      }

      private async void Button_Video_Clicked(object sender, EventArgs e)
      {
         //Video
         await MediaPlayer.Current.Play("http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4");
      }

      public IList<string> Mp3UrlList => new[]
      {
         "https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3",
         "https://ia800605.us.archive.org/32/items/Mp3Playlist_555/CelineDion-IfICould.mp3",
         "https://ia800605.us.archive.org/32/items/Mp3Playlist_555/Daughtry-Homeacoustic.mp3",
         "https://storage.googleapis.com/uamp/The_Kyoto_Connection_-_Wake_Up/01_-_Intro_-_The_Way_Of_Waking_Up_feat_Alan_Watts.mp3",
         "https://aphid.fireside.fm/d/1437767933/02d84890-e58d-43eb-ab4c-26bcc8524289/d9b38b7f-5ede-4ca7-a5d6-a18d5605aba1.mp3"
      };

      private async void Button_PlayMultiple_Clicked(object sender, EventArgs e)
      {
         //await MediaPlayer.Current.PlayList.Play(Mp3UrlList);
      }

      private async void Button_PlayMultipleShort_Clicked(object sender, EventArgs e)
      {
         await MediaPlayer.Current.Play("http://www.zpf.fr/podcast/01.mp3");
         MediaPlayer.Current.Playlist.Add(await MediaItem.GetNew("http://www.zpf.fr/podcast/02.mp3", MediaType.Audio, MediaLocation.Remote));
         MediaPlayer.Current.Playlist.Add(await MediaItem.GetNew("http://www.zpf.fr/podcast/03.mp3", MediaType.Audio, MediaLocation.Remote));
         MediaPlayer.Current.Playlist.Add(await MediaItem.GetNew("http://www.zpf.fr/podcast/04.mp3", MediaType.Audio, MediaLocation.Remote));
         MediaPlayer.Current.Playlist.Add(await MediaItem.GetNew("http://www.zpf.fr/podcast/05.mp3", MediaType.Audio, MediaLocation.Remote));
      }

      private async void Button_Audio1_Clicked(object sender, EventArgs e)
      {
         await MediaPlayer.Current.Play("http://www.zpf.fr/podcast/01.mp3");
      }

      private async void Button_Audio2_Clicked(object sender, EventArgs e)
      {
         await MediaPlayer.Current.Play("http://www.zpf.fr/podcast/02.mp3");
      }

      private async void Button_Audio3_Clicked(object sender, EventArgs e)
      {
         await MediaPlayer.Current.Play("http://www.zpf.fr/podcast/03.mp3");
      }

      private async void Button_PlayPause_Clicked(object sender, EventArgs e)
      {
         //ToDo: await MediaPlayer.Current.PlayPause();

         switch (MediaPlayer.Current.State)
         {
            case MediaPlayerState.Stopped:
            case MediaPlayerState.Paused:
               await MediaPlayer.Current.Play();
               break;

            case MediaPlayerState.Playing:
               await MediaPlayer.Current.Pause();
               break;
         }
      }

      private async void Button_Stop_Clicked(object sender, EventArgs e)
      {
         await MediaPlayer.Current.Stop();
      }

      private async void Button_StepBackward_Clicked(object sender, EventArgs e)
      {
         await MediaPlayer.Current.StepBackward();
      }

      private async void Button_StepForward_Clicked(object sender, EventArgs e)
      {
         await MediaPlayer.Current.StepForward();
      }

      private async void Button_PlayPrevious_Clicked(object sender, EventArgs e)
      {
         await MediaPlayer.Current.Playlist.PlayPrevious();
      }

      private async void Button_PlayNext_Clicked(object sender, EventArgs e)
      {
         await MediaPlayer.Current.Playlist.PlayNext();
      }

      private async void Button_PlayEntry_Clicked(object sender, EventArgs e)
      {
         await MediaPlayer.Current.Play(entryURI.Text);
      }
   }
}
