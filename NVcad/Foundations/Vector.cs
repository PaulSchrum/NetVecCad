using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Angles;

namespace NVcad.Foundations
{
   [Serializable]
   public struct Vector
   {
      public Double x { get; set; }
      public Double y { get; set; }
      public Double? z { get; set; }

      public Vector(double x_, double y_, double? z_) : this()
      {
         x = x_; y = y_; z = z_;
      }

      public Vector(Point beginPt, Point endPoint) : this()
      {
         x = endPoint.x - beginPt.x;
         y = endPoint.y - beginPt.y;
         z = endPoint.z - endPoint.z;
      }

      public Vector(Azimuth direction, Double length) : this()
      {
         x = length * Math.Sin(direction.angle_);
         y = length * Math.Cos(direction.angle_);
         z = null;
      }

      public void flattenThisZ()
      {
         this.z = null;
      }

      public Vector flattenZnew()
      {
         return new Vector(this.x, this.y, null);
      }

      public Point plus(Point aPoint)
      {
         return new Point(aPoint.x + this.x, aPoint.y + this.y, aPoint.z.Value + this.z.Value);
      }

      public Azimuth Azimuth
      {
         get
         {
            return new Azimuth(Math.Atan2(y, x));
         }
         private set { }
      }

      public Double Length
      {
         get { return Math.Sqrt(x * x + y * y + (Double) z * (Double) z); }
         private set { }
      }

      public Azimuth DirectionHorizontal
      {
         get { return new Azimuth(Math.Atan2(y, x)); }
         private set { }
      }

      public double dotProduct(Vector otherVec)
      {
         return (this.x * otherVec.x) + (this.y * otherVec.y) + (this.z.Value * otherVec.z.Value);
      }

      public Vector crossProduct(Vector otherVec)
      {
         Vector newVec = new Vector();
         newVec.x = this.y * otherVec.z.Value - this.z.Value * otherVec.y;
         newVec.y = this.z.Value * otherVec.x - this.x * otherVec.z.Value;
         newVec.z = this.x * otherVec.y - this.y * otherVec.x;
         return newVec;
      }

      public static Vector operator +(Vector vec1, Deflection defl)
      {
         Azimuth newAz = vec1.Azimuth + defl;
         Vector newVec = new Vector(newAz, vec1.Length);
         newVec.z = vec1.z;

         return newVec;
      }

      public override String ToString()
      {
         var retStr = new StringBuilder(String.Format("L: {0:#.0000}, Az: ", this.Length));
         retStr.Append(this.Azimuth.ToString());
         return retStr.ToString();
      }


   }
}
