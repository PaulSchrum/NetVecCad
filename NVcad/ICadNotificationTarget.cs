using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NVcad.CadObjects;

namespace NVcad
{
   public interface ICadNotificationTarget
   {
      void DrawGraphicItem(NVcad.CadObjects.Graphic graphicItem);
   }
}
