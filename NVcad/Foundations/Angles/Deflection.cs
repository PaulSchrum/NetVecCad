using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations;

namespace NVcad.Foundations.Angles
{
   [Serializable]
   sealed public class Deflection : NVcad.Foundations.Angle
   {
      public int deflectionDirection
      {
         get { return Math.Sign(base.angle__); }
      }

      private bool isInternalSolution 
      {
         get { return (Math.Abs(base.angle__) < Math.PI); }
      }

      public Deflection() { }

      public Deflection(Azimuth BeginAzimuth, Azimuth EndAzimuth, bool assumeInternalSolution)
      {
         Double angle;

         // first assume internal solution
         if(BeginAzimuth.angle_ > EndAzimuth.angle_)
         {
            if ((BeginAzimuth.angle_ - EndAzimuth.angle_) > 180.0)
            {
               angle = (EndAzimuth.angle_ + 360.0) - BeginAzimuth.angle_;
            }
            else
            {
               angle = EndAzimuth.angle_ - BeginAzimuth.angle_;
            }
         }
         else // EndAzimuth >= BeginAzimuth
         {
            if ((EndAzimuth.angle_ - BeginAzimuth.angle_) > 180.0)
            {
               angle = (EndAzimuth.angle_ - BeginAzimuth.angle_) - 360.0;
            }
            else
            {
               angle = BeginAzimuth.angle_ - EndAzimuth.angle_;
            }
         }

         this.angle_ = angle.ToRadians();
         if (false == assumeInternalSolution)
            this.ForceToExternalSolution();
      }

      public static Deflection ctorDeflectionFromAngle(double angleDegrees, int deflectionDirection)
      {
         return new Deflection(Angle.radiansFromDegree(angleDegrees), deflectionDirection);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="anAngleDbl">unit: Radians</param>
      /// <param name="deflectionSign">+1 for Right, -1 for left</param>
      public Deflection(double anAngleDbl, int deflectionDirection)
      {
         this.angle_ = deflectionDirection * anAngleDbl;
      }

      public Deflection(Angle anAngle)
      {
         this.angle_ = anAngle.angle_;
      }

      internal override double angle_
      {
         get { return angle__; }
         set 
         {
            this.angle__ = normalizeBetweenPlusMinus2PI(value);
         }
      }

      private static Double normalizeBetweenPlusMinus2PI(Double val)
      {
         Double returnVal = val;
         if (returnVal >= Angle.TwoPI())
         {
            returnVal = Angle.ComputeRemainderScaledByDenominator(returnVal, Angle.TwoPI());
         }
         else if (returnVal <= Angle.TwoPI().NAbs())
         {
            returnVal =
               Angle.ComputeRemainderScaledByDenominator(-1.0 * returnVal, Angle.TwoPI());
            returnVal = -1.0 * returnVal;
         }
         return returnVal;
      }

      /// <summary>
      /// Intended for use mainly with Deflections generated from
      /// subtracting two Azimuths.  The direction gets swapped
      /// and the value gets subtracted from 360 degrees.
      /// </summary>
      public void ForceToExternalSolution()
      {
         if (Math.Abs(this.angle_) > Math.PI) 
            return; // since it's already external solution

         Double angle = this.angle_;
         int prevSign = Math.Sign(angle);
         angle = Angle.TwoPI() - Math.Abs(angle);
         this.angle_ = -1 * prevSign * angle;
      }

      public override void setFromDegreesDouble(double deg)
      {
         this.angle_ = deg.ToRadians();
      }

      public override double getAsRadians()
      {
         return this.angle__;
      }

      public override void setFromDegreesMinutesSeconds(int degrees, int minutes, double seconds)
      {
         double min = Math.Abs(minutes) + (Math.Abs(seconds) / 60.0);
         double deg = Math.Abs(degrees) + (min / 60.0);
         this.angle_ = deg.ToRadians() * Math.Sign(degrees);
      }

      public override double getAsDegreesDouble()
      {
         return this.angle__.ToDegrees();
      }

      public static Deflection operator *(Deflection defl, Double multiplier)
      {
         return new Deflection(defl.angle__ * multiplier);
      }

      public static Deflection operator /(Deflection defl, Double divisor)
      {
         return new Deflection(defl.angle__ / divisor);
      }

      public static implicit operator Deflection(double radianDbl)
      {
         var returnDeflection = new Deflection();
         returnDeflection.angle__ = normalizeBetweenPlusMinus2PI(radianDbl);
         return returnDeflection;
      }

      public static implicit operator Deflection(Degree degrees)
      {
         return new Deflection(degrees.getAsRadians());
      }

      public static Deflection NewFromDoubleDegrees(Double asDegrees)
      {
         return new Deflection(asDegrees.ToRadians());
      }

      public override string ToString()
      {
         var str = Math.Abs(this.getAsDegreesDouble()).ToString() + "°";
         if (this.deflectionDirection > 0) str = str + " RT";
         else str = str + " LT";
         return str;
      }
   }
}
