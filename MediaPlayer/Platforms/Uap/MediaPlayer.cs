using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;

namespace ZPF.Media
{
   public class MediaPlayer : MediaPlayerBase
   {
      private readonly Windows.Media.Playback.MediaPlayer _player;
      private IMediaExtractor _MediaExtractor;

      public MediaPlayer()
      {
         _player = new Windows.Media.Playback.MediaPlayer();
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      static MediaPlayer _Current = null;

      public static MediaPlayer Current
      {
         get
         {
            if (_Current == null)
            {
               _Current = new MediaPlayer();
            };

            return _Current;
         }

         set
         {
            _Current = value;
         }
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public new void Init()
      {
         // uap

         IsInitialized = true;
      }

      //public new bool play(string uri)
      //{
      //   _player.Source = MediaSource.CreateFromUri(new Uri(uri));
      //   _player.Play();
      //   return true;
      //}

      public async new Task<IMediaItem> Play(string uri)
      {
         var mediaItem = await MediaExtractor.CreateMediaItem(uri);

         var mediaPlaybackList = new MediaPlaybackList();
         var mediaSource = await CreateMediaSource(mediaItem);
         var item = new MediaPlaybackItem(mediaSource);
         mediaPlaybackList.Items.Add(item);
         _player.Source = mediaPlaybackList;
         _player.Play();

         return mediaItem;
      }

      private async Task<MediaSource> CreateMediaSource(IMediaItem mediaItem)
      {
         switch (mediaItem.MediaLocation)
         {
            case MediaLocation.Remote:
               return MediaSource.CreateFromUri(new Uri(mediaItem.MediaUri));

            case MediaLocation.FileSystem:
               var du = _player.SystemMediaTransportControls.DisplayUpdater;
               var storageFile = await StorageFile.GetFileFromPathAsync(mediaItem.MediaUri);
               var playbackType = (mediaItem.MediaType == MediaType.Audio ? Windows.Media.MediaPlaybackType.Music : Windows.Media.MediaPlaybackType.Video);
               await du.CopyFromFileAsync(playbackType, storageFile);
               du.Update();
               return MediaSource.CreateFromStorageFile(storageFile);
         }

         return MediaSource.CreateFromUri(new Uri(mediaItem.MediaUri));
      }
   }
}
