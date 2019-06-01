using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ZPF.Media
{
   public class MediaExtractor : IMediaExtractor
   {
      public async Task<IMediaItem> CreateMediaItem(string url)
      {
         IMediaItem mediaItem = new MediaItem(url);
         return await ExtractMetadata(mediaItem);

         //var taskSource = new TaskCompletionSource<IMediaItem>();

         //IMediaItem mediaItem = new MediaItem(url);

         //taskSource.SetResult(mediaItem);
         //return taskSource.Task;
      }

      public async Task<IMediaItem> CreateMediaItem(FileInfo file)
      {
         IMediaItem mediaItem = new MediaItem(file.FullName);
         return await ExtractMetadata(mediaItem);
      }

      public async Task<IMediaItem> CreateMediaItem(IMediaItem mediaItem)
      {
         return await ExtractMetadata(mediaItem);
      }

      public async Task<IMediaItem> ExtractMetadata(IMediaItem mediaItem)
      {  
         // default title
         mediaItem.Title = System.IO.Path.GetFileNameWithoutExtension(mediaItem.MediaUri);

         mediaItem.IsMetadataExtracted = true;

         return mediaItem;
      }
   }
}
