using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace NVcad.Foundations.Coordinates
{
   [Serializable]
   public struct Point
   {
      public Double x { get; set; }
      public Double y { get; set; }
      public Double? z { get; set; }

      public Point(Point otherPt)
         : this()
      {
         x = otherPt.x; y = otherPt.y; z = otherPt.z;
      }

      public Point(double X, double Y, double Z)
         : this()
      {
         x = X; y = Y; z = Z;
      }

      public Point(String X, String Y, String Z)
         : this()
      {
         x = Double.Parse(X);
         y = Double.Parse(Y);
         z = Double.Parse(Z);
      }

      public Point(double X, double Y)
         : this()
      {
         x = X; y = Y; z = null;
      }

      public bool is2D()
      {
         return (this.z == null) ? true : false;
      }

      public bool is3d()
      {
         return !is2D();
      }

      public static Vector operator -(Point p1, Point p2)
      {
         return new Vector(p2.x - p1.x, p2.y - p1.y, p2.z - p1.z);
      }

      public static Point operator +(Point point, Vector vector)
      {
         if (point.z != null && vector.z != null)
            return new Point(point.x + vector.x, point.y + vector.y, point.z.Value + vector.z.Value);

         Double? nulldbl = null;
         return new Point(point.x + vector.x, point.y + vector.y);
      }

      public Double GetHorizontalDistanceTo(Point other)
      {
         Double dx = other.x - this.x;
         Double dy = other.y - this.y;
         return Math.Sqrt(dx * dx + dy * dy);
      }

      public int compareByXthenY(Point other)
      {
         int xComp; int yComp;
         xComp = this.x.CompareTo(other.x);
         if (xComp == 0)
         {
            yComp = this.y.CompareTo(other.y);
            return yComp;
         }
         return xComp;
      }

      public int compareByYthenX(Point other)
      {
         int xComp; int yComp;
         yComp = this.y.CompareTo(other.y);
         if (yComp == 0)
         {
            xComp = this.x.CompareTo(other.x);
            return xComp;
         }
         return yComp;
      }

      public override string ToString()
      {
         if (null == z)
         {
            return x.ToString(NumberFormatInfo.InvariantInfo) + ", " + y.ToString(NumberFormatInfo.InvariantInfo);
         }
         Double dblZ = z.Value;
         return x.ToString(NumberFormatInfo.InvariantInfo) + ", " + y.ToString(NumberFormatInfo.InvariantInfo) + ", " +
            dblZ.ToString(NumberFormatInfo.InvariantInfo);
      }

      public string ToStringSpaceDelimited()
      {
         if (null == z)
         {
            return x.ToString(NumberFormatInfo.InvariantInfo) + " " + y.ToString(NumberFormatInfo.InvariantInfo);
         }
         Double dblZ = z.Value;
         return x.ToString(NumberFormatInfo.InvariantInfo) + " " + y.ToString(NumberFormatInfo.InvariantInfo) + " " +
            dblZ.ToString(NumberFormatInfo.InvariantInfo);
      }

   }
}
