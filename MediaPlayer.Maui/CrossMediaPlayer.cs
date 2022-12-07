using System;

namespace ZPF.Media
{
   /// <summary>
   /// 
   /// </summary>
   public static class MediaPlayer
   {
      static IMediaPlayer _MediaPlayer = null;

      public static void Init(IMediaPlayer mediaPlayer)
      {
         _MediaPlayer = mediaPlayer;
      }

      /// <summary>
      /// Gets if the plugin is supported on the current platform.
      /// </summary>
      public static bool IsSupported => _MediaPlayer == null ? false : true;

      /// <summary>
      /// Current plugin implementation to use
      /// </summary>
      public static IMediaPlayer Current
      {
         get
         {
            var ret = _MediaPlayer;
            if (ret == null)
            {
               throw NotImplementedInReferenceAssembly();
            }
            return ret;
         }
      }

      static IMediaPlayer CreateMediaPlayer()
      {
#if _YEAP_
         return new MediaPlayerImplementation();
#else
         return null;
#endif
      }

      internal static Exception NotImplementedInReferenceAssembly() =>
         new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
   }
}
