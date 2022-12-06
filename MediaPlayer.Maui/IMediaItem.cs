﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZPF.Media
{
   public enum DownloadStatus
   {
      Downloaded,
      Downloading,
      NotDownloaded
   }

   public enum MediaType
   {
      Default,
      Audio,
      Video,
      Dash,
      Hls,
      SmoothStreaming
   }

   public enum MediaLocation
   {
      Default,
      Remote,
      FileSystem,
      Embedded
   }

   public interface IMediaItem : INotifyPropertyChanged
   {
      /// <summary>
      /// Gets or sets a value indicating whether [metadata extracted].
      /// </summary>
      /// <value>
      ///   <c>true</c> if [metadata extracted]; otherwise, <c>false</c>.
      /// </value>
      bool IsMetadataExtracted { get; set; }

      ///// <summary>
      ///// Raised when MediaItem is updated
      ///// </summary>
      //event MetadataUpdatedEventHandler MetadataUpdated;

      /// <summary>
      /// The metadata key for a int typed value to retrieve the information about whether the media is an advertisement.
      /// </summary>
      string Advertisement { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the Album title for the media.
      /// </summary>
      string Album { get; set; }

      /// <summary>
      /// The metadata key for a Bitmap typed value to retrieve the information about the artwork for the Album of the media's original source.
      /// </summary>
      object AlbumArt { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the artist for the Album of the media's original source.
      /// </summary>
      string AlbumArtist { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the Uri of the artwork for the Album of the media's original source.
      /// </summary>
      string AlbumArtUri { get; set; }

      /// <summary>
      /// The metadata key for a Bitmap typed value to retrieve the information about the artwork for the media.
      /// </summary>
      object Art { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the artist of the media.
      /// </summary>
      string Artist { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about Uri of the artwork for the media.
      /// </summary>
      string ArtUri { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the author of the media.
      /// </summary>
      string Author { get; set; }

      ///// <summary>
      ///// The metadata key for a int typed value to retrieve the information about the bluetooth folder type of the media specified in the section 6.10.2.2 of the Bluetooth AVRCP 1.5.
      ///// </summary>
      //BtFolderType BtFolderType { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the compilation status of the media.
      /// </summary>
      string Compilation { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the composer of the media.
      /// </summary>
      string Composer { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the date the media was created or published.
      /// </summary>
      string Date { get; set; }

      /// <summary>
      /// The metadata key for a int typed value to retrieve the information about the disc number for the media's original source.
      /// </summary>
      int DiscNumber { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the description that is suitable for display to the user.
      /// </summary>
      string DisplayDescription { get; set; }

      /// <summary>
      /// The metadata key for a Bitmap typed value to retrieve the information about the icon or thumbnail that is suitable for display to the user.
      /// </summary>
      object DisplayIcon { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the Uri of icon or thumbnail that is suitable for display to the user.
      /// </summary>
      string DisplayIconUri { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the subtitle that is suitable for display to the user.
      /// </summary>
      string DisplaySubtitle { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the title that is suitable for display to the user.
      /// </summary>
      string DisplayTitle { get; set; }

      /// <summary>
      /// The metadata key for a int typed value to retrieve the information about the download status of the media which will be used for later offline playback.
      /// </summary>
      DownloadStatus DownloadStatus { get; set; }

      /// <summary>
      /// The metadata key for a int typed value to retrieve the information about the duration of the media in ms.
      /// </summary>
      TimeSpan Duration { get; set; }

      /// <summary>
      /// A Bundle extra.
      /// </summary>
      object Extras { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the genre of the media.
      /// </summary>
      string Genre { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the media ID of the content.
      /// </summary>
      string MediaId { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the Uri of the content.
      /// </summary>
      string MediaUri { get; set; }

      /// <summary>
      /// The metadata key for a int typed value to retrieve the information about the number of tracks in the media's original source.
      /// </summary>
      int NumTracks { get; set; }

      /// <summary>
      /// The metadata key for a Rating2 typed value to retrieve the information about the overall rating for the media.
      /// </summary>
      object Rating { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the title of the media.
      /// </summary>
      string Title { get; set; }

      /// <summary>
      /// The metadata key for a int typed value to retrieve the information about the track number for the media.
      /// </summary>
      int TrackNumber { get; set; }

      /// <summary>
      /// The metadata key for a Rating2 typed value to retrieve the information about the user's rating for the media.
      /// </summary>
      object UserRating { get; set; }

      /// <summary>
      /// The metadata key for a CharSequence or string typed value to retrieve the information about the writer of the media.
      /// </summary>
      string Writer { get; set; }

      /// <summary>
      /// The metadata key for a int typed value to retrieve the information about the year the media was created or published.
      /// </summary>
      int Year { get; set; }

      /// <summary>
      /// The file extension of the media item
      /// This may not be available for every item
      /// </summary>
      string FileExtension { get; set; }

      /// <summary>
      /// The type of the media item
      /// Standard Type is Default which will try to play in the standard way.
      /// </summary>
      MediaType MediaType { get; set; }

      /// <summary>
      /// The location of the media item
      /// Standard location is Default which will make a guess based on the URI.
      /// </summary>
      MediaLocation MediaLocation { get; set; }
   }

}
