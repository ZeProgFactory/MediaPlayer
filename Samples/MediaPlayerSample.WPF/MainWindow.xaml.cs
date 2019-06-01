using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace MediaPlayerSample.WPF
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : FormsApplicationPage
   {
      public MainWindow()
      {
         InitializeComponent();

         // - - -  - - - 

         ZPF.Media.MediaPlayer.Current.Init(this);
         System.Windows.Controls.MediaElement Player = (System.Windows.Controls.MediaElement)ZPF.Media.MediaPlayer.Current.Player;

         Player.Volume = 1;

         // - - -  - - -

         Forms.Init();
         LoadApplication(new MediaPlayerSample.App());
      }
   }
}
