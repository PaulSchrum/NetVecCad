using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NVcad.Foundations.Coordinates;
using NVcad.CadObjects;

namespace NVcad.Models
{
   public class Model : IBoundingBoxed
   {
      protected BoundingBox BoundingBox { get; set; }
      protected List<Graphic> allGrahics { get; set; }  // maybe refactor later to concurrent collection
      

      public BoundingBox getBoundingBox()
      {
         return BoundingBox;
      }

      public void setUpTestingModel_20140422()
      {
         allGrahics = new List<Graphic>();
         allGrahics.Add(new LineSegment(2.0, 3.0, 12.0, 20.0));
         allGrahics.Add(new LineSegment(1.0, 7.0, 8.0, 4.0));
      }
   }
}
