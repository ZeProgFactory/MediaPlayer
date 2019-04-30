using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZPF.Media
{
   public class MPlayerBase : IMPlayer
   {
      public void Init()
      {
         throw new NotImplementedException();
      }

      public Task<IMediaItem> Play(string URL)
      {
         throw new NotImplementedException();
      }
   }
}
