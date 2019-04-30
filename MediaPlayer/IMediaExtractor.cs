using System.IO;
using System.Threading.Tasks;

namespace ZPF.Media
{
   public interface IMediaExtractor
   {
      Task<IMediaItem> CreateMediaItem(string url);

      Task<IMediaItem> CreateMediaItem(FileInfo file);

      Task<IMediaItem> CreateMediaItem(IMediaItem mediaItem);
   }
}
