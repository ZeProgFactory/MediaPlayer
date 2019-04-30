using System;
using System.Threading.Tasks;

namespace ZPF.Media
{
   public interface ITestMP
   {
      void Init();

      bool Play(string URL);
      Task<IMediaItem> Play2(string URL);
   }

   public class TestBaseMP : ITestMP
   {
      public void Init()
      {
         throw new NotImplementedException();
      }

      public bool Play(string URL)
      {
         throw new NotImplementedException();
      }

      public Task<IMediaItem> Play2(string URL)
      {
         throw new NotImplementedException();
      }
   }
}


