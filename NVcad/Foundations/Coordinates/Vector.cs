using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Angles;


namespace NVcad.Foundations.Coordinates
{
   [Serializable]
   public class Vector : ICloneable
   {
      public Double x { get; set; }
      public Double y { get; set; }
      public Double? z { get; set; }

      public Vector() { }

      public Vector(double x_, double y_, double? z_)
         : this()
      {
         x = x_; y = y_; z = z_;
      }

      public Vector(Vector other) : this()
      {
         x = other.x; y = other.y; z = other.z;
      }

      public Vector(Point beginPt, Point endPoint)
         : this()
      {
         x = endPoint.x - beginPt.x;
         y = endPoint.y - beginPt.y;
         z = endPoint.z - endPoint.z;
      }

      public Vector(Azimuth direction, Double length)
         : this()
      {
         x = length * Math.Sin(direction.getAsRadians());
         y = length * Math.Cos(direction.getAsRadians());
         z = null;
      }

      public Vector(Double dx, Double dy)
         : this()
      {
         x = dx;
         y = dy;
      }

      public Object Clone()
      {
         Vector clone = new Vector();
         clone.x = this.x; clone.y = this.y; clone.z = this.z;
         return clone;
      }

      public void flattenThisZ()
      {
         this.z = null;
      }

      public Vector flattenZnew()
      {
         return new Vector(this.x, this.y, null);
      }

      public Double getAbsMax()
      {
         Double intermedSecond;
         intermedSecond = (this.z == null) ? 
            Math.Abs(this.y) :
            Math.Max(Math.Abs(this.y), Math.Abs((Double) this.z));
         return Math.Max(Math.Abs(this.x), intermedSecond);
      }

      public Double getScaleToMatch_maximized(Vector other)
      {
         return 
            (this.getScaleToMatch(other)).getAbsMax();
      }

      public Vector getScaleToMatch(Vector other)
      {
         Vector retVec = new Vector();
         retVec.x = other.x / this.x;
         retVec.y = other.y / this.y;
         retVec.z = other.z / this.z;

         return retVec;
      }

      public Point plus(Point aPoint)
      {
         return new Point(aPoint.x + this.x, aPoint.y + this.y, aPoint.z.Value + this.z.Value);
      }

      public Azimuth Azimuth
      {
         get
         {
            Angle angle = new Angle(Math.Atan2(y, x));
            return Azimuth.CreateFromCadConventionAngle(angle);
         }
         private set { }
      }

      public Double Length
      {
         get { return getLen(); }
         set 
         {
            Double prevLen = getLen();
            if(0.0 != prevLen)
            {
               Double scaleFactor = value / prevLen;
               this.ScaleBy(scaleFactor, scaleFactor, scaleFactor);
            }
            // else -- 0-length vector - undefined meaning,
            // thus no action -- leave as it is, silently.
         }
      }

      private Double getLen()
      {
         var zSquared = (null == z) ? 0.0 :
            (Double) (z * z);
         return Math.Sqrt(x * x + y * y + zSquared);
      }

      public Azimuth DirectionHorizontal
      {
         get { return new Azimuth(Math.Atan2(y, x)); }
         private set { }
      }

      public Vector rotateCloneAboutZ(Angle rotation)
      {
         var newVec = new Vector(this); //(Vector) this.Clone();
         newVec.rotateAboutZ(rotation);
         return newVec;
      }

      public void rotateAboutZ(Angle rotation)
      {
         Double rotRad = rotation.angle_;
         Double vecAngle = Math.Atan2(this.y, this.x);
         vecAngle += rotRad;
         Double lngth = this.Length;
         x = lngth * Math.Sin(vecAngle);
         y = lngth * Math.Cos(vecAngle);
         //Double rotCos = rotation.cos(); Double rotSin = rotation.sin();
         //Double newthis_x = this.x * rotation.cos() - this.y * rotation.sin();
         //this.y = this.x * rotation.sin() + this.y * rotation.cos();
         //this.x = newthis_x;
      }

      public void flipAboutX_2d()
      {
         this.y *= -1.0;
      }

      public void flipAboutY_2d()
      {
         this.x *= -1.0;
      }

      public void reverseDirection()
      {
         this.ScaleBy(-1.0, -1.0, -1.0);
      }

      public void ScaleBy(Double xScale, Double yScale, Double? zScale)
      {
         this.x *= xScale; this.y *= yScale; this.z *= zScale;
      }

      public void ScaleBy(Double scale)
      {
         ScaleBy(scale, scale, scale);
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