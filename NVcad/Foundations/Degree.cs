using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Angles;

namespace NVcad.Foundations
{
   [Serializable]
   public class Degree
   {
      private readonly Double degrees_;
      public Degree(Double newVal)
      {
         this.degrees_ = newVal;
      }

      public Double getAsRadians()
      {
         return degrees_ * Math.PI / 180.0;
      }

      public Double getAsDouble()
      {
         return degrees_;
      }

      public static Degree newFromRadians(Double rad)
      {
         return new Degree(180.0 * rad / Math.PI);
      }

      public static Degree newFromDegreesMinutesSeconds(int degrees, int minutes, double seconds)
      {
         return new Degree(
               Math.Abs((double)degrees) +
               (double)minutes / 60.0 + seconds / 3600.0
                        );
      }

      public static Degree Asin(Double val)
      {
         return newFromRadians(Math.Asin(val));
      }

      public static Degree Acos(Double val)
      {
         return newFromRadians(Math.Acos(val));
      }

      public static Degree Atan(Double val)
      {
         return newFromRadians(Math.Atan(val));
      }

      public static Degree Atan2(Double y, Double x)
      {
         return newFromRadians(Math.Atan2(y, x));
      }

      public static Double Sin(Degree deg)
      {
         return Math.Sin(deg.getAsRadians());
      }

      public static Double Cos(Degree deg)
      {
         return Math.Cos(deg.getAsRadians());
      }

      public static Double Tan(Degree deg)
      {
         return Math.Tan(deg.getAsRadians());
      }

      public static Degree Abs(Degree deg)
      {
         return Math.Abs(deg.degrees_);
      }

      public static implicit operator Degree(double doubleVal)
      {
         return new Degree(doubleVal);
      }

      public static bool operator >=(Degree left, Degree right)
      {
         return left.degrees_ >= right.degrees_;
      }

      public static bool operator <=(Degree left, Degree right)
      {
         return left.degrees_ <= right.degrees_;
      }

      public static Degree operator +(Degree left, Double right)
      {
         return left.degrees_ + right;
      }

      public static Degree operator -(Degree left, Degree right)
      {
         return left.degrees_ - right.degrees_;
      }

      public static Degree operator -(Degree left, Double right)
      {
         return left.degrees_ - right;
      }

      public static Degree operator -(Degree left, Deflection right)
      {
         return left.degrees_ - right.getAsDegrees();
      }

      public static Degree operator *(Degree left, Double right)
      {
         return left.degrees_ * right;
      }

      public override string ToString()
      {
         return degrees_.ToString() + "°";
      }
   }

   public static class extendDoubleForPtsDegree
   {
      public static Degree AsPtsDegree(this Double val)
      {
         return new Degree(val);
      }

      public static Dictionary<String, Double> AsParts(this Double val)
      {
         var str = val.ToString().Split('.');
         Dictionary<String, Double> result = new Dictionary<string, Double>(2);
         result.Add("Integer Part", Convert.ToDouble(str[0]));
         if (str[1].Length > 18)
            result.Add("Fractional Part", Convert.ToDouble("0." + str[1].Substring(0, 18)));
         else
            result.Add("Fractional Part", Convert.ToDouble("0." + str[1]));
         return result;
      }

      public static Double ToRadians(this Double valDegrees)
      {
         return valDegrees * Math.PI / 180.0;
      }

      public static Double ToDegrees(this Double valRadians)
      {
         return valRadians / Math.PI * 180.0;
      }

   }

}
