using System;

namespace ZPF.Media
{
   /// <summary>
   /// 
   /// </summary>
   public static class ZeMediaPlayer
   {
      static IMediaPlayer _ZeMediaPlayer = null;

      public static void Init(IMediaPlayer mediaPlayer)
      {
         _ZeMediaPlayer = mediaPlayer;
      }

      /// <summary>
      /// Gets if the plugin is supported on the current platform.
      /// </summary>
      public static bool IsSupported => _ZeMediaPlayer == null ? false : true;

      /// <summary>
      /// Current plugin implementation to use
      /// </summary>
      public static IMediaPlayer Current
      {
         get
         {
            var ret = _ZeMediaPlayer;
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
