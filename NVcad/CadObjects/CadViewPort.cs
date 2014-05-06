using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVcad.CadObjects
{
   public class CadViewPort : Graphic
   {
      public Double Height { get; set; }
      public Double Width { get; set; }
      // base.Origin is the view center.
      // base.Rotation is the view rotation.
      // base.Scale is the view scale.

      protected void updateBoundingBox()
      {
          
      }
   }
}
