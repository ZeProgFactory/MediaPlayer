using System.Threading.Tasks;

namespace ZPF.Media
{
   public interface IMPlayer
   {
      ///// <summary>
      ///// Native mediaplayer ...
      ///// </summary>
      //object Player { get; }


      //IMediaExtractor MediaExtractor { get; set; }
      ////IVolumeManager VolumeManager { get; set; }

      void Init();
      Task<IMediaItem> Play(string URL);
   }
}


