using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MediaPlayerSample.Droid
{
   [Activity(Label = "MediaPlayerSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate(savedInstanceState);

         // - - -  - - - 
         // OK MonoDroid 8.1
         //Android.Media.MediaPlayer player = new Android.Media.MediaPlayer();
         //player.Prepared += (s, e) =>
         //{
         //   player.Start();
         //};

         //player.Error += (sender, args) =>
         //{
         //   //playback error
         //   Console.WriteLine("Error in playback resetting: " + args.What);
         //   player.Stop();//this will clean up and reset properly.
         //};

         //player.Info += (object sender, Android.Media.MediaPlayer.InfoEventArgs e) =>
         //{
         //   Console.WriteLine(e.What);
         //};

         ////player.SetAudioStreamType(Stream.Music);
         //player.SetDataSource("http://freesound.org/data/previews/273/273629_4068345-lq.mp3");
         //player.Prepare();

         // - - -  - - - 


         Xamarin.Essentials.Platform.Init(this, savedInstanceState);
         global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
         LoadApplication(new App());
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
      {
         Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

         base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }
   }
}
