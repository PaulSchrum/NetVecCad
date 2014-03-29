using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVcad.Foundations.Angles
{
   public class Slope : Angle
   {
      public Slope()
      {
         setFromXY(1.0, 0.0);
      }

      public Slope(double slopeASaDouble)
      {
         setFromXY(1.0, slopeASaDouble);
      }

      /* */
      public Slope FlipDirection()
      {
         if (this.isVertical() == true)
            return this;

         return new Slope(-1.0 * Math.Asin(angle_));

      } /* */

      public bool isVertical()
      {
         if (Math.Abs(Math.Sin(angle_)) == 1.0)
            return true;
         else
            return false;
      }

      public bool isSlopeUp()
      {
         double asDegrees = this.getAsDegreesDouble();
         return ((asDegrees > 0.0) || (asDegrees < 180.0));
      }

      public static implicit operator double(Slope aSlope)
      {
         double testForVerticality;
         testForVerticality = Math.Sin(aSlope.angle_);

         if (testForVerticality == 1.0)
            return Double.PositiveInfinity;
         else if (testForVerticality == -1.0)
            return Double.NegativeInfinity;
         else return Math.Tan(aSlope.angle_);

      }

      public static implicit operator Slope(double slopeAs_double)
      {
         Slope aSlope = new Slope(slopeAs_double);
         return aSlope;
      }

      public double getAsSlope()
      {
         if (this.isVertical() == true)
            return Double.PositiveInfinity;

         return Math.Tan(angle_);
      }

      public override string ToString()
      {
         if (this.isVertical() == true)
            return "Vertical";

         double slopeValue = this.getAsSlope();
         if (Math.Abs(slopeValue) <= 0.1)
         {
            if (slopeValue > 0.0)
               return String.Format("+{0:0.0%}", slopeValue);
            else
               return String.Format("{0:0.0%}", slopeValue);
         }
         else
            return String.Format("{0:0.0} : 1", 1 / slopeValue);
      }
   }
}
