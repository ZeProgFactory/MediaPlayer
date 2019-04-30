using System;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;

namespace ZPF.Media
{
   public class MPlayer : MPlayerBase
   {
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
         // dummy
         throw new NotImplementedException();
      }

      public new bool Play(string URL)
      {
         throw new NotImplementedException();
      }
   }
}
