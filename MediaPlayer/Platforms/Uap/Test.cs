using System;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;

namespace ZPF.Media
{
   public class TestMP : TestBaseMP
   {
      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      static TestMP _Current = null;

      public static TestMP Current
      {
         get
         {
            if (_Current == null)
            {
               _Current = new TestMP();
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

      public new bool Play(string uri)
      {
         return true;
      }

      public new async Task<IMediaItem> Play2(string uri)
      {
         await StorageFile.GetFileFromPathAsync(uri);
         return null;
      }
   }
}
