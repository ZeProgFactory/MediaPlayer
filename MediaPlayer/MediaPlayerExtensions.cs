using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZPF.Media
{
   static class MediaPlayerExtensions
   {
      public static bool IsPlaying(this IMediaPlayer mediaPlayer)
      {
         return mediaPlayer.State == MediaPlayerState.Playing;
      }

      public static bool IsBuffering(this IMediaPlayer mediaPlayer)
      {
         return mediaPlayer.State == MediaPlayerState.Buffering;
      }

      public static Task PlayPause(this IMediaPlayer mediaPlayer)
      {
         var status = mediaPlayer.State;

         if (status == MediaPlayerState.Paused || status == MediaPlayerState.Stopped)
            return mediaPlayer.Play();
         else
            return mediaPlayer.Pause();
      }
   }
}
