using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Coordinates;

namespace NVcad.Foundations
{
   [Serializable]
   public class Angle
   {
      protected double angle__;
      //private Point point1;
      //private Point point2;

      public static Angle WHOLECIRCLE 
      {
         get { return new Angle(2.0 * Math.PI); } 
         private set{}
      }

      public static Angle DEGREE
      {
         get { return new Angle(Math.PI / 180.0); }
         private set { }
      }

      public static Angle RADIAN
      {
         get { return new Angle(1.0); }
         private set { }
      }

      public Angle() { }

      public static Angle AngleFactory(Double dx, Double dy)
      {
         Angle newAngle = new Angle();
         newAngle.angle__ = Math.Atan2(dy, dx);
         return newAngle;
      }

      public static Angle AngleFactory(Double valueAsDegrees)
      {
         Angle newAngle = new Angle();
         newAngle.setFromDegreesDouble(valueAsDegrees);
         return newAngle;
      }

      public static Angle FromTurns(Double turns)
      {
         return new Angle(2.0 * Math.PI * turns);
      }

      public Angle(Double valueAsRadians)
      {
         angle__ = valueAsRadians;
         normalize(valueAsRadians);
      }

      public Angle(double radius, double degreeOfCurveLength)
      {
         angle__ = degreeOfCurveLength / radius;
      }

      // needs unit test
      public Angle(Point point1, Point point2)
      {
         // TODO: Complete member initialization
         //this.point1 = point1;
         //this.point2 = point2;
         setFromXY(point2.x - point1.x, point2.y - point1.y);
      }
      internal virtual double angle_ { get { return angle__; } set { normalize(value); } }
      //private static double angleScratchPad;

      public virtual double getAsRadians() { return angle_; }

      public virtual double getAsDegreesDouble()
      {
         return 180.0 * angle_ / Math.PI;
      }

      public virtual Degree getAsDegrees()
      {
         return Degree.newFromRadians(angle_);
      }

      public virtual void setFromDegreesDouble(double degrees)
      {
         angle_ = Math.PI * degrees / 180.0;
      }

      public static double radiansFromDegree(double degrees)
      {
         return Math.PI * degrees / 180.0;
      }

      public static double degreesFromRadians(double radians)
      {
         return 180.0 * radians / Math.PI;
      }

      public virtual void setFromDegreesMinutesSeconds(int degrees, int minutes, double seconds)
      {
         setFromDegreesDouble(
               (double) degrees + (double) minutes / 60.0 + seconds / 3600.0
                        );
      }

      public void setFromDMSstring(string angleInDMS)
      {
         throw new NotImplementedException();
      }

      public virtual void setFromXY(double x, double y)
      {
         double dbl = Math.Atan2(y, x);
         angle_ = dbl;
      }

      protected double fp(double val)
      {
         if (val > 0.0)
            return val - Math.Floor(val);
         else if (val < 0.0)
            return -1.0 * (val - Math.Floor(val));
         else
            return 0.0;
      }

      protected void normalize(double anAngle)
      {
         //angleScratchPad = anAngle / Math.PI;
         //angle__ = fp(angleScratchPad) * Math.PI;
         double oldAngle = anAngle * 180 / Math.PI;

         // approach to normalize #1, probably too slow
         angle__ = Math.Atan2(Math.Sin(anAngle), Math.Cos(anAngle));

         // approach to normalizae #2, can't get it to work -- will one day
         //int sign = Math.Sign(anAngle);
         //angleScratchPad = (anAngle * sign) / Math.PI;
         //angle__ = (fp(angleScratchPad) * sign) * Math.PI;
      }

      protected void normalizeToPlusOrMinus2Pi(Double anAngle)
      {
         Double TwoPi = Math.PI * 2.0;
         angle__ = Angle.ComputeRemainderScaledByDenominator(anAngle, TwoPi);
      }

      public static Double normalizeToPlusOrMinus2PiStatic(Double anAngle)
      {
         return ComputeRemainderScaledByDenominator(anAngle, 2 * Math.PI);
      }

      public static Double normalizeToPlusOrMinus360Static(Double val)
      {
         return ComputeRemainderScaledByDenominator(val, 360.0);
      }

      public static Double ComputeRemainderScaledByDenominator(Double numerator, double denominator)  
      {
         Double sgn = Math.Sign(numerator);
         Double ratio = numerator / denominator;
         ratio = Math.Abs(ratio);
         Double fractionPart;
         fractionPart = 1 + ratio - Math.Round(ratio, MidpointRounding.AwayFromZero);
         if (sgn < 0.0)
         {
            fractionPart = fractionPart - 2;
            if (fractionPart < 1.0)
               fractionPart = -1.0 * (fractionPart + 2);
               //1.0 + ratio - Math.Round(ratio, MidpointRounding.AwayFromZero);
         }

         Double returnDouble = sgn * Math.Abs(fractionPart) * Math.Abs(denominator);
         return returnDouble;
      }

      public override string ToString()
      {
         return (angle__ * 180 / Math.PI).ToString();
      }

      public static Angle operator +(Angle angle1, Angle angle2)
      {
         var retVal = new Angle();
         retVal.normalize(angle1.angle__ + angle2.angle__);
         return retVal;
      }

      public static Angle operator -(Angle angle1, Angle angle2)
      {
         var retVal = new Angle();
         retVal.normalize( angle1.angle__ - angle2.angle__);
         return retVal;
      }

      public Angle multiply(Double multiplier)
      {
         return new Angle(this.angle__ * multiplier);
      }
      
      public Double cos()
      {
         return Math.Cos(this.angle__);
      }

      public Double sin()
      {
         return Math.Sin(this.angle__);
      }

      public Double tan()
      {
         return Math.Tan(this.angle__);
      }

      // operator overloads
      public static implicit operator Angle(double angleAs_double)
      {
         Angle anAngle = new Angle();
         anAngle.angle_ = angleAs_double;
         return anAngle;
      }

      public static implicit operator Angle(Vector angleAs_vector)
      {
         Angle anAngle = new Angle();
         anAngle.angle__ = Math.Atan2(angleAs_vector.y, angleAs_vector.x);
         return anAngle;
      }
   }
}
