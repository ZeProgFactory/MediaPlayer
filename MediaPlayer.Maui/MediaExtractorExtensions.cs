using System.Linq;
using System.Threading.Tasks;

namespace ZPF.Media
{
   public static class MediaExtractorExtensions
   {
      public static async Task<IMediaItem> FetchMetaData(this IMediaItem mediaItem)
      {
         if (mediaItem.IsMetadataExtracted)
            return mediaItem;

         return mediaItem = await ZeMediaPlayer.Current.MediaExtractor.CreateMediaItem(mediaItem);
      }

      public static async Task<IMediaItem[]> FetchMetaData(this IPlaylist playlist)
      {
         var mediaItems = playlist.Select(i => i.FetchMetaData());

         return await Task.WhenAll(mediaItems);
      }
   }
}
