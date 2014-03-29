using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Coordinates;

namespace NVcad.Foundations.Angles
{
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

      public new double angle_ { get { return getAsAzimuth(); } set { base.normalize(value); } }

      public Azimuth reverse()
      {
         return new Azimuth(this.angle__ + Math.PI);
      }
      
      private double getAsAzimuth()
      {
         double retVal;

         retVal = (-1.0 * base.angle_) + (Math.PI / 2.0);

         return retVal;
      }

      public override double getAsDegreesDouble()
      {
         double retValueDbl = getAsAzimuth() * 180 / Math.PI;
         return retValueDbl >= 0.0 ? retValueDbl : retValueDbl + 360.0;
      }

      public override Degree getAsDegrees()
      {
         Degree retValueDeg = getAsAzimuth() * 180 / Math.PI;
         return retValueDeg >= 0.0 ? retValueDeg : retValueDeg + 360.0;
      }

      public override void setFromDegreesDouble(double degrees)
      {
         //double adjustedDegrees = ((degrees / -180.0)+ 1) *180.0;
         double radians = degrees * Math.PI / 180.0;
         angle_ = Math.Atan2(Math.Cos(radians), Math.Sin(radians));  // This is flipped intentionally

      }

      public override void setFromDegreesMinutesSeconds(int degrees, int minutes, double seconds)
      {
         setFromDegreesDouble(
               (double)degrees + (double)minutes / 60.0 + seconds / 3600.0
                        );
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

      public static Azimuth newAzimuthFromAngle(Angle angle)
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
         Double returnDeflection = (Az1.angle_ - Az2.angle_);
         return Angle.normalizeToPlusOrMinus2PiStatic(returnDeflection);
      }

      public static Azimuth operator +(Azimuth Az1, Deflection defl)
      {
         var newAzDeg = Az1.getAsDegreesDouble() + defl.getAsDegreesDouble();
         Double retDbl = Angle.normalizeToPlusOrMinus360Static(newAzDeg);
         Azimuth retAz = new Azimuth();
         retAz.setFromDegreesDouble(retDbl);
         return retAz;
      }

      public Deflection minus(Azimuth Az2)
      {
         Double returnDeflection = (this.angle_ - Az2.angle_);
         return new Deflection(Angle.normalizeToPlusOrMinus2PiStatic(returnDeflection));
      }

      public override String ToString()
      {
         return String.Format("{0:0.0000}°", this.getAsDegreesDouble());
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
