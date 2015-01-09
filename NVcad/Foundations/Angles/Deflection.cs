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
      private int deflectionDirection_;
      public int deflectionDirection
      {
         get { return deflectionDirection_; }
         set
         {
            deflectionDirection_ = value >= 0 ? 1 : -1;
         }
      }

      private bool isInternalSolution { get; set; }  // start here.  make tests get 
      // a tuple of internal and external solutions for each subtraction
      // also add method to force external solution and another one
      // to force internal solution

      public Deflection() { }

      public Deflection(Azimuth BeginAzimuth, Azimuth EndAzimuth, bool assumeInternalSolution)
      {
         isInternalSolution = assumeInternalSolution;

         if (false == isInternalSolution)
            this.angle_ = BeginAzimuth - EndAzimuth;
         else
            this.angle_ = EndAzimuth - BeginAzimuth;

         this.deflectionDirection = 1;
         if (this.angle_ < 0.0 || this.angle_ > Math.PI)
         {
            this.deflectionDirection = -1;
         }
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
         base.angle_ = Math.Abs(anAngleDbl);
         this.deflectionDirection = deflectionDirection;
         //base.angle__ = ptsAngle.normalizeToPlusOrMinus2PiStatic(anAngleDbl);
         //angle_ = anAngleDbl;
      }

      public Deflection(Angle anAngle)
      {
         this.deflectionDirection = Math.Sign(anAngle.angle_);
         angle__ = Math.Abs(anAngle.angle_);
      }

      internal override double angle_
      {
         get
         {
            Double retAngle = angle__;

            if (deflectionDirection >= 0)
            {
               if (retAngle < 0.0)
                  retAngle += 2.0 * Math.PI;
            }
            else
            {
               if (retAngle < 0.0)
                  retAngle += 2.0 * Math.PI;

               retAngle *= -1.0;
            }

            return retAngle;
         }
         set { normalize(value); }
      }

      /// <summary>
      /// Intended for use mainly with Deflections generated from
      /// subtracting two Azimuths.  The direction gets swapped
      /// and the value gets subtracted from 360 degrees.
      /// </summary>
      public void ForceToExternalSolution()
      {
         if (this.isInternalSolution == false) return;
         var complement = Math.Abs((2 * Math.PI) - this.getAsRadians());
         base.angle_ = complement;
         this.deflectionDirection *= -1;
         this.isInternalSolution = false;
      }

      public override void setFromDegreesDouble(double deg)
      {
         base.setFromDegreesDouble(Math.Abs(deg));
         this.deflectionDirection = Math.Sign(deg);
      }

      public override double getAsRadians()
      {
         Double retVal = base.getAsRadians();
         if (this.deflectionDirection < 0)
         {
            if (this.isInternalSolution == true)
            {
               retVal = retVal + 2.0 * Math.PI;
               retVal *= -1.0;
            }
         }
         return retVal;
      }

      public override void setFromDegreesMinutesSeconds(int degrees, int minutes, double seconds)
      {
         setFromDegreesDouble(
               Math.Abs((double)degrees) +
               (double)minutes / 60.0 + seconds / 3600.0
                        );
         deflectionDirection = Math.Sign(degrees);
      }

      public override double getAsDegreesDouble()
      {
         return 180.0 * this.getAsRadians() / Math.PI;
      }

      public static Deflection operator *(Deflection defl, Double multiplier)
      {
         Deflection retDefl = new Deflection();
         retDefl.angle_ = defl.angle_ * multiplier;
         retDefl.deflectionDirection_ = defl.deflectionDirection_;
         return retDefl;
      }

      public static Deflection operator /(Deflection defl, Double divisor)
      {
         if (defl.deflectionDirection_ > 0)
            return defl * (1 / divisor);
         else
         { // Deflection is negative
            Deflection retDefl = new Deflection();
            retDefl.deflectionDirection_ = defl.deflectionDirection_;
            //retDefl.isLessThanEqual_180degrees = true;
            retDefl.angle__ = defl.angle__ / divisor;
            return retDefl;
         }
      }

      public static implicit operator Deflection(double radianDbl)
      {
         return new Deflection(radianDbl);
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
         var str = this.getAsDegreesDouble().ToString() + "°";
         if (this.deflectionDirection > 0) str = str + " RT";
         else str = str + " LT";
         return str;
      }
   }
}
