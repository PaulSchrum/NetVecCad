using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Coordinates;

namespace NVcad.Foundations.Angles
{
   /// <summary>
   /// Special type of Angle indicating direction on a compass.
   /// Due north is 0 degrees; Due east is 90 degrees.
   /// Values must fall between 0 degrees and 359.9999999999999 degrees.
   /// </summary>
   sealed public class Azimuth : Angle
   {
      public Azimuth() { }

      public Azimuth(double anAngleDbl)
      {
         angle_ = anAngleDbl;
      }

      public Azimuth(Degree deg)
      {
         this.angle__ = Math.Atan2(Degree.Sin(deg), Degree.Cos(deg));
      }

      public Azimuth(Point beginPt, Point endPt)
      {
         this.angle__ = Math.Atan2(endPt.y - beginPt.y, endPt.x - beginPt.x);
      }

      public new double angle_ { get { return getAsAzimuth(); } set { this.normalizeAndSetAngle(value); } }

      public static Azimuth NewFromDoubleAsDegrees(Double az)
      {
         Azimuth returnAz = new Azimuth();
         returnAz.setFromDegreesDouble(az);
         return returnAz;
      }

      public Azimuth reverse()
      {
         return new Azimuth(this.angle__ + Math.PI);
      }
      
      private void normalizeAndSetAngle(Double val)
      {
         Double AngleAsTurns = val / (Math.PI * 2.0);
         var fractionalPart = AngleAsTurns.FP();
         if(val < 0.0)
         {
            fractionalPart = (-1.0 * fractionalPart) + 1.0;
         }
         this.angle__ = fractionalPart * Math.PI * 2.0;
      }

      private double getAsAzimuth()
      {
         double retVal;

         retVal = this.getAsDegreesDouble();

         return retVal;
      }

      public override double getAsDegreesDouble()
      {
         return angle__.ToDegrees();
      }

      public override Degree getAsDegrees()
      {
         Degree retValueDeg = angle_.ToDegrees();
         return retValueDeg;
      }

      public override void setFromDegreesDouble(double degrees)
      {
         angle_ = degrees.ToRadians();
      }

      public override void setFromDegreesMinutesSeconds(int degrees, int minutes, double seconds)
      {
         setFromDegreesDouble(
               (double)degrees + (double)minutes / 60.0 + seconds / 3600.0
                        );
      }

      public override void setFromXY(double x, double y)
      {
         double dbl = Math.Atan2(x, y);
         if (dbl < 0.0) dbl += TwoPI();
         angle_ = dbl;
      }

      public Angle GetAsCadConventionAngle()
      {
         Angle retVal;
         Double angl = this.angle__;

         if (angl < PIover2 * 3)
            retVal = new Angle(PIover2 - angl);
         else
            retVal = new Angle(PIover2 * 5 - angl);

         return retVal;
      }

      public static Azimuth CreateFromCadConventionAngle(Angle angle)
      {
         Azimuth retVal;
         if (angle.angle_ <= PIover2)
            retVal = new Azimuth(PIover2 - angle.angle_);
         else
            retVal = new Azimuth(PIover2 * 5 - angle.angle_);
         return retVal;
      }

      public static int getQuadrant(double angleDegrees)
      {
         return (int)Math.Round((angleDegrees / 90.0) + 0.5);
      }

      //to do:
      //setAsAzimuth
      //getAsDegreeMinuteSecond
      //setAsDegree
      //setAsDegreeMinuteSecond
      //yada

      public static Azimuth ctorAzimuthFromDegree(Double deg)
      {
         Azimuth retAz = new Azimuth();
         retAz.setFromDegreesDouble(deg);
         return retAz;
      }

      public static Azimuth ctorAzimuthFromAngle(Angle angle)
      {
         Azimuth retAz = new Azimuth();
         retAz.setFromDegreesDouble(angle.getAsDegreesDouble());
         return retAz;
      }

      // operator overloads
      public static implicit operator Azimuth(double angleAs_double)
      {
         Azimuth anAzimuth = new Azimuth();
         anAzimuth.angle_ = angleAs_double;
         return anAzimuth;
      }

      public static Azimuth operator +(Azimuth anAz, Angle anAngle)
      {
         return new Azimuth(anAz.getAsRadians() - anAngle.getAsRadians());  // Note: Subtraction is intentional since azimuths are clockwise
      }

      public static double operator -(Azimuth Az1, Azimuth Az2)
      {
         Double returnDeflection = (Az1.angle__ - Az2.angle__);
         return Angle.normalizeToPlusOrMinus2PiStatic(returnDeflection);
      }

      public static Azimuth operator +(Azimuth Az1, Deflection defl)
      {
         var Az1Dbl = Az1.angle__;
         var deflDbl = defl.getAsRadians();
         var newAzDeg = Az1Dbl + deflDbl;
         if (newAzDeg > TwoPI()) newAzDeg -= TwoPI();
         if (newAzDeg < 0.0) newAzDeg += TwoPI();
         Azimuth retAz = new Azimuth(newAzDeg);
         return retAz;
      }

      public static Azimuth operator -(Azimuth Az1, Deflection defl)
      {
         var newAzDeg = Az1.getAsDegreesDouble() - defl.getAsDegreesDouble();
         Double retDbl = Angle.normalizeToPlusOrMinus360Static(newAzDeg);
         Azimuth retAz = new Azimuth();
         retAz.setFromDegreesDouble(retDbl);
         return retAz;
      }

      /// <summary>
      /// Always assumes that the desired answer is the internal solution.
      /// If the external solution is desired, 
      /// call deflection.ForceToExternalSolution()
      /// immediately after calling minus().
      /// </summary>
      /// <param name="Az2"></param>
      /// <returns></returns>
      public Deflection minus(Azimuth Az2)
      {
         Deflection returnDeflection = 
            new Deflection(this, Az2, true);

         return returnDeflection;
      }

      public override String ToString()
      {
         return String.Format("{0:0.0000}°", getAsDegreesDouble());
      }
   }

   public static class extendDoubleForAzimuth
   {
      public static Azimuth AsAzimuth(this Double val)
      {
         return new Azimuth(val);
      }
   }

}
