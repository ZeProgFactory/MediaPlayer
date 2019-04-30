using System;
using System.Threading.Tasks;

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
         // dummy
         throw new NotImplementedException();
      }

      public new bool Play(string URL)
      {
         throw new NotImplementedException();
      }

      public new async Task<IMediaItem> Play2(string uri)
      {
         throw new NotImplementedException();
      }
   }
}
