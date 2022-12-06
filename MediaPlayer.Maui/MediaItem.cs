﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ZPF.Media
{
   public class MediaItem : IMediaItem
   {
      public MediaItem(string uri)
      {
         if (string.IsNullOrEmpty(uri))
            throw new ArgumentNullException(uri);

         MediaUri = uri;

         // default title
         Title = System.IO.Path.GetFileNameWithoutExtension(MediaUri);
      }

      public string Advertisement { get; set; }
      public string Album { get; set; }
      public object AlbumArt { get; set; }
      public string AlbumArtist { get; set; }
      public string AlbumArtUri { get; set; }
      public object Art { get; set; }
      public string Artist { get; set; }
      public string ArtUri { get; set; }
      public string Author { get; set; }
      //public BtFolderType BtFolderType { get; set; } = BtFolderType.Mixed;
      public string Compilation { get; set; }
      public string Composer { get; set; }
      public string Date { get; set; }
      public int DiscNumber { get; set; }
      public string DisplayDescription { get; set; }
      public object DisplayIcon { get; set; }
      public string DisplayIconUri { get; set; }
      public string DisplaySubtitle { get; set; }
      public string DisplayTitle { get; set; }
      public DownloadStatus DownloadStatus { get; set; } = DownloadStatus.NotDownloaded;
      public TimeSpan Duration { get; set; }
      public object Extras { get; set; }
      public string Genre { get; set; }
      public string MediaId { get; set; } = Guid.NewGuid().ToString();
      public string MediaUri { get; set; }
      public int NumTracks { get; set; }
      public object Rating { get; set; }
      public string Title { get; set; }
      public int TrackNumber { get; set; }
      public object UserRating { get; set; }
      public string Writer { get; set; }
      public int Year { get; set; }

      public string FileExtension { get; set; }
      public MediaType MediaType { get; set; } = MediaType.Default;
      public MediaLocation MediaLocation { get; set; } = MediaLocation.Default;

      private bool _isMetadataExtracted = false;
      public bool IsMetadataExtracted
      {
         get
         {
            return _isMetadataExtracted;
         }
         set
         {
            _isMetadataExtracted = value;
            //MetadataUpdated?.Invoke(this, new MetadataChangedEventArgs(this));
         }
      }

      //public event MetadataUpdatedEventHandler MetadataUpdated;

      //TODO: Update all properties to use this
      public event PropertyChangedEventHandler PropertyChanged;

      public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - - 

      public static async Task<IMediaItem> GetNew(string path, MediaType mediaType, MediaLocation mediaLocation)
      {
         var mediaItem = new MediaItem(path)
         {
            MediaType = mediaType,
            MediaLocation = mediaLocation,
         };

         return await MediaPlayer.Current.MediaExtractor.CreateMediaItem(mediaItem);
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - - 

      public override string ToString()
      {
         return (string.IsNullOrEmpty(Title) ? (string.IsNullOrEmpty(DisplayTitle) ? MediaUri : DisplayTitle) : Title);
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - - 
   }
}
