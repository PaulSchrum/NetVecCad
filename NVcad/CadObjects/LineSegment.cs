using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NVcad.Foundations;
using NVcad.Foundations.Coordinates;


namespace NVcad.CadObjects
{
   public class LineSegment : Path
   {
      public LineSegment() { }

      public LineSegment(Double X1, Double Y1, Double X2, Double Y2)
      {
         this.Origin = new Point(X1, Y1);
         this.EndPoint = new Point(X2, Y2);
         this.BoundingBox = new BoundingBox(this.Origin);
         this.BoundingBox.expandByPoint(this.EndPoint);
      }

      public override Angle Rotation
      {
         get
         {
            return new Angle(this.Origin, this.EndPoint);
         }
         set
         { }
      }

      public override double Scale
      {
         get
         {  return 1.0; }
         set
         { }
      }

   }
}
