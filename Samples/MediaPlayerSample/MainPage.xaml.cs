using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZPF.Media;

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

         MediaPlayer.Current.PlayList.RepeatMode = RepeatMode.Off;;
         MediaPlayer.Current.PlayList.ShuffleMode = ShuffleMode.Off;

         // Hook into events
         MediaPlayer.Current.StateChanged += Current_StateChanged;
         MediaPlayer.Current.PositionChanged += Current_PositionChanged;

         MediaPlayer.Current.PlayingChanged += Current_PlayingChanged;
         MediaPlayer.Current.BufferingChanged += Current_BufferingChanged;

         MediaPlayer.Current.MediaItemFinished += Current_MediaItemFinished;
         MediaPlayer.Current.MediaItemChanged += Current_MediaItemChanged;
      }

      private void Current_PlayingChanged(object sender, PlayingChangedEventArgs e)
      {
         //Bug: Is running on Startup without any previous operation
         Debug.WriteLine($"Current_PlayingChanged {e.Position}");
      }

      private void Current_BufferingChanged(object sender, BufferingChangedEventArgs e)
      {
         throw new NotImplementedException();
      }

      private void Current_MediaItemFinished(object sender, MediaItemEventArgs e)
      {
         throw new NotImplementedException();
      }

      private void Current_MediaItemChanged(object sender, MediaItemEventArgs e)
      {
         Debug.WriteLine("Current_MediaItemChanged");
      }

      private void Current_PositionChanged(object sender, PositionChangedEventArgs e)
      {
         Device.BeginInvokeOnMainThread(() =>
         {
            labelPos.Text = $"{MediaPlayer.Current.Position}";
         });
      }

      private void Current_StateChanged(object sender, StateChangedEventArgs e)
      {
         Device.BeginInvokeOnMainThread(() =>
         {
            //labelInfo.Text = $"{e.State} {MediaPlayer.Current.Duration} {MediaPlayer.Current.MediaQueue.Current?.Title}";
            labelInfo.Text = $"{e.State} {MediaPlayer.Current.Duration} ";
         });
      }

      private async void Button_Audio_Clicked(object sender, EventArgs e)
      {
         //Audio
         //await MediaPlayer.Current.Play("https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");

         await MediaPlayer.Current.Play("http://freesound.org/data/previews/273/273629_4068345-lq.mp3");
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
         await MediaPlayer.Current.PlayList.PlayPrevious();
      }

      private async void Button_PlayNext_Clicked(object sender, EventArgs e)
      {
         await MediaPlayer.Current.PlayList.PlayNext();
      }
   }
}
