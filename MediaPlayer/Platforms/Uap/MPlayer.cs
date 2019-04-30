using System;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;

namespace ZPF.Media
{
   public class MPlayer : MPlayerBase
   {
      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      static MPlayer _Current = null;

      public static MPlayer Current
      {
         get
         {
            if (_Current == null)
            {
               _Current = new MPlayer();
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
      }

      public new async Task<IMediaItem> Play(string uri)
      {
         //var mediaItem = await MediaExtractor.CreateMediaItem(uri);

         //var mediaPlaybackList = new MediaPlaybackList();
         //var mediaSource = await CreateMediaSource(mediaItem);
         //var item = new MediaPlaybackItem(mediaSource);
         //mediaPlaybackList.Items.Add(item);
         //_player.Source = mediaPlaybackList;
         //_player.Play();

         //return mediaItem;
         return null;
      }

      //private async Task<MediaSource> CreateMediaSource(IMediaItem mediaItem)
      //{
      //   switch (mediaItem.MediaLocation)
      //   {
      //      case MediaLocation.Remote:
      //         return MediaSource.CreateFromUri(new Uri(mediaItem.MediaUri));

      //      case MediaLocation.FileSystem:
      //         var du = _player.SystemMediaTransportControls.DisplayUpdater;
      //         var storageFile = await StorageFile.GetFileFromPathAsync(mediaItem.MediaUri);
      //         var playbackType = (mediaItem.MediaType == MediaType.Audio ? Windows.Media.MediaPlaybackType.Music : Windows.Media.MediaPlaybackType.Video);
      //         await du.CopyFromFileAsync(playbackType, storageFile);
      //         du.Update();
      //         return MediaSource.CreateFromStorageFile(storageFile);
      //   }

      //   return MediaSource.CreateFromUri(new Uri(mediaItem.MediaUri));
      //}


   }
}
