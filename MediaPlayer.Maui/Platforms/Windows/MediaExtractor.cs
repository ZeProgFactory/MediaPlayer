﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace ZPF.Media.Uap
{
   public class MediaExtractor : IMediaExtractor
   {
      //protected Dictionary<string, string> RequestHeaders => MediaPlayer.Current.RequestHeaders;

      public MediaExtractor()
      {
      }

      public virtual async Task<IMediaItem> CreateMediaItem(string url)
      {
         IMediaItem mediaItem = new MediaItem(url);
         return await ExtractMetadata(mediaItem);
      }

      public virtual async Task<IMediaItem> CreateMediaItem(FileInfo file)
      {
         IMediaItem mediaItem = new MediaItem(file.FullName);
         return await ExtractMetadata(mediaItem);
      }

      public virtual async Task<IMediaItem> CreateMediaItem(IMediaItem mediaItem)
      {
         return await ExtractMetadata(mediaItem);
      }

      public async Task<IMediaItem> ExtractMetadata(IMediaItem mediaItem)
      {
         // default title
         mediaItem.Title = System.IO.Path.GetFileNameWithoutExtension(mediaItem.MediaUri);

         if (mediaItem.MediaLocation == MediaLocation.FileSystem)
         {
            var file = await StorageFile.GetFileFromPathAsync(mediaItem.MediaUri);

            switch (mediaItem.MediaType)
            {
               case MediaType.Audio:
                  await SetAudioInfo(file, mediaItem);
                  break;

               case MediaType.Video:
                  await SetVideoInfo(file, mediaItem);
                  break;
            }

            await SetAlbumArt(file, mediaItem);
         }

         mediaItem.IsMetadataExtracted = true;

         return mediaItem;
      }

      private async Task SetAudioInfo(IStorageItemProperties file, IMediaItem mediaItem)
      {
         var musicProperties = await file.Properties.GetMusicPropertiesAsync();

         mediaItem.Title = musicProperties.Title;
         mediaItem.Artist = musicProperties.Artist;
         mediaItem.Album = musicProperties.Album;
      }

      private async Task SetVideoInfo(IStorageItemProperties file, IMediaItem mediaItem)
      {
         var musicProperties = await file.Properties.GetVideoPropertiesAsync();
         mediaItem.Title = musicProperties.Title;
      }

      private async Task SetAlbumArt(IStorageItemProperties file, IMediaItem mediaItem)
      {
         var isAudio = mediaItem.MediaType == MediaType.Audio;
         var thumbnailMode = isAudio ? ThumbnailMode.MusicView : ThumbnailMode.VideosView;

         var thumbnail = await file.GetThumbnailAsync(thumbnailMode);
         if (thumbnail != null && thumbnail.Type == ThumbnailType.Image)
         {
            BitmapSource bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(thumbnail);
            mediaItem.AlbumArt = bitmap;
         }
      }
   }
}
