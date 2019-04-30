using System;

namespace ZPF.Media
{
   /// <summary>
   /// 
   /// </summary>
   public static class MediaPlayer
   {
      static Lazy<IMediaPlayer> implementation = new Lazy<IMediaPlayer>(() => CreateMediaPlayer(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

      /// <summary>
      /// Gets if the plugin is supported on the current platform.
      /// </summary>
      public static bool IsSupported => implementation.Value == null ? false : true;

      /// <summary>
      /// Current plugin implementation to use
      /// </summary>
      public static IMediaPlayer Current
      {
         get
         {
            var ret = implementation.Value;
            if (ret == null)
            {
               throw NotImplementedInReferenceAssembly();
            }
            return ret;
         }
      }

      static IMediaPlayer CreateMediaPlayer()
      {
#if NETSTANDARD1_0 || NETSTANDARD2_0
         return null;
#else
         return new MediaPlayerImplementation();
#endif
      }

      internal static Exception NotImplementedInReferenceAssembly() =>
         new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
   }
}
