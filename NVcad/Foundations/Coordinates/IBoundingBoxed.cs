using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVcad.Foundations.Coordinates
{
   public interface IBoundingBoxed
   {
      BoundingBox getBoundingBox();
   }
}
