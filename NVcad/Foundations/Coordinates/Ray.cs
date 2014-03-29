using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Angles;

namespace NVcad.Foundations.Coordinates
{
   public class Ray
   {
      public Point StartPoint { get; set; }
      public Slope Slope { get; set; }
      private int advanceDirection_  = 1;
      public int advanceDirection 
      { get { return advanceDirection_; } 
         set 
         {
            advanceDirection_ = Math.Sign(value);
            if (0 == value) advanceDirection_ = 1;
         } 
      }
      public Azimuth HorizontalDirection { get; set; }

      public double? getElevationAlong(double X)
      {
         if (true == Slope.isVertical())
            return null;

         double horizDistance = X - StartPoint.x;

         if (Math.Sign(horizDistance) != Math.Sign(Slope.getAsSlope()))
            return null;

         return (double?)
            ((horizDistance * Slope.getAsSlope()) + this.StartPoint.z);

      }

      public double get_m() { return this.Slope.getAsSlope() * this.advanceDirection; }
      public double get_b()
      {
         if (true == Slope.isVertical())
            return Double.NaN;

         return this.StartPoint.z.Value - (StartPoint.x * Slope.getAsSlope() * this.advanceDirection);
      }

      public bool isWithinDomain(double testX)
      {
         if (true == Slope.isVertical())
            return (testX == this.StartPoint.x);

         int sign = Math.Sign(testX - this.StartPoint.x);

         if (Math.Sign(testX - this.StartPoint.x) == this.advanceDirection )
            return true;

         return false;
      }

      public double getOffset(Point endPt)
      {
         Vector directVectr = endPt - this.StartPoint;
         Angle alpha =  this.HorizontalDirection - directVectr;
         Double offset = -1.0 * directVectr.Length * Math.Sin(alpha.getAsRadians());
         return offset;
      }
   }
}
