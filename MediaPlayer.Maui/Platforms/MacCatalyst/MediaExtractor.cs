﻿using System;
using System.Collections.Generic;
using System.IO;
using AVFoundation;
using Foundation;
using System.Threading.Tasks;
#if __IOS__ || __TVOS__
using UIKit;
#endif

namespace ZPF.Media
{
   class MediaExtractor : IMediaExtractor
   {
      //protected Dictionary<string, string> RequestHeaders => CrossMediaManager.Current.RequestHeaders;

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
         var assetsToLoad = new List<string>
            {
                AVMetadata.CommonKeyArtist,
                AVMetadata.CommonKeyTitle,
                AVMetadata.CommonKeyArtwork
            };

#if __IOS__
         var url = GetUrlFor(mediaItem);
#else
         var url = new NSUrl(mediaItem.MediaUri);
#endif

         // Default title to filename
         // mediaItem.Title = url.LastPathComponent;

         var asset = AVAsset.FromUrl(url);
         await asset.LoadValuesTaskAsync(assetsToLoad.ToArray());

         foreach (var avMetadataItem in asset.CommonMetadata)
         {
            if (avMetadataItem.CommonKey == AVMetadata.CommonKeyArtist)
            {
               mediaItem.Artist = ((NSString)avMetadataItem.Value).ToString();
            }
            else if (avMetadataItem.CommonKey == AVMetadata.CommonKeyTitle)
            {
               mediaItem.Title = ((NSString)avMetadataItem.Value).ToString();
            }
            else if (avMetadataItem.CommonKey == AVMetadata.CommonKeyArtwork)
            {
#if __IOS__ || __TVOS__
               var image = UIImage.LoadFromData(avMetadataItem.DataValue);
               mediaItem.AlbumArt = image;
#endif
            }
         }

         mediaItem.IsMetadataExtracted = true;

         return mediaItem;
      }

#if __IOS__
      public static NSUrl GetUrlFor(IMediaItem mediaItem)
      {
         var isLocallyAvailable = mediaItem.MediaLocation == MediaLocation.FileSystem;

         var url = isLocallyAvailable ? new NSUrl(mediaItem.MediaUri, false) : new NSUrl(mediaItem.MediaUri);

         return url;
      }
#endif
   }
}

