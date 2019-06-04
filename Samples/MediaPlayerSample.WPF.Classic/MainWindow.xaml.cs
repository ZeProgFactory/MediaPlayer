using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ZPF.Media;

namespace MediaPlayerSample.WPF.Classic
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      void ExceptionButNotFatal()
      {
         System.Windows.Controls.MediaElement Player = new MediaElement();

         Player.LoadedBehavior = MediaState.Play;
         Player.UnloadedBehavior = MediaState.Manual;
         Player.Volume = 1;
         Player.IsMuted = false;

         //OK Player.Source = new Uri("http://freesound.org/data/previews/273/273629_4068345-lq.mp3");
         Player.Source = new Uri("https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");

         try
         {
            Player.Play();
         }
         catch
         {
            // shit happens
         };
      }

      async void MediaPlayerCurrentPlay_OK()
      {
         MediaPlayer.Current.Init(this);
         System.Windows.Controls.MediaElement Player = (System.Windows.Controls.MediaElement)ZPF.Media.MediaPlayer.Current.Player;

         // Audio
         await MediaPlayer.Current.Play("http://freesound.org/data/previews/273/273629_4068345-lq.mp3");
      }

      async void MediaPlayerCurrentPlay_Exception01_OK()
      {
         MediaPlayer.Current.Init(this);
         System.Windows.Controls.MediaElement Player = (System.Windows.Controls.MediaElement)ZPF.Media.MediaPlayer.Current.Player;

         try
         {
            Player.Source = new Uri("https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
            Player.Play();
         }
         catch
         {
            // shit happens
         };
      }

      async void MediaPlayerCurrentPlay_Exception02_OK()
      {
         MediaPlayer.Current.Init(this);
         System.Windows.Controls.MediaElement Player = (System.Windows.Controls.MediaElement)ZPF.Media.MediaPlayer.Current.Player;

         try
         {
            Player.Source = new Uri("https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
            await MediaPlayer.Current.Play();
         }
         catch
         {
            // shit happens
         };
      }

      async void MediaPlayerCurrentPlay_FatalException()
      {
         MediaPlayer.Current.Init(this);
         System.Windows.Controls.MediaElement Player = (System.Windows.Controls.MediaElement)ZPF.Media.MediaPlayer.Current.Player;

         try
         {
            // Audio
            await MediaPlayer.Current.Play("https://ia800806.us.archive.org/15/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3");
         }
         catch (Exception ex)
         {
            Debug.WriteLine(ex.Message);
         };
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         // ExceptionButNotFatal()
         // MediaPlayerCurrentPlay_OK();

         MediaPlayerCurrentPlay_FatalException();
      }
   }
}
