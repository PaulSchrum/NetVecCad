using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVcad.Foundations.Coordinates
{
   [Serializable]
   public class BoundingBox
   {
      public Point lowerLeftPt { get; set; }
      public Point upperRightPt { get; set; }
      private bool isInitialized { get; set; }

      internal BoundingBox() { isInitialized = false; }

      public BoundingBox(Double LLx, Double LLy, Double URx, Double URy)
      {
         lowerLeftPt = new Point(LLx, LLy);
         upperRightPt = new Point(URx, URy);
         isInitialized = true;
      }

      public BoundingBox(Point aPoint)
      {
         lowerLeftPt = new Point(aPoint);
         upperRightPt = new Point(aPoint);
         isInitialized = true;
      }

      public void expandByBox(BoundingBox other)
      {
         if (this.isInitialized == false)
         {
            lowerLeftPt = new Point(other.lowerLeftPt.x, other.lowerLeftPt.y, other.lowerLeftPt.z);
            upperRightPt = new Point(other.upperRightPt.x, other.upperRightPt.y, other.upperRightPt.z);
            this.isInitialized = true;
         }
         else
         {
            if (other.lowerLeftPt.x < lowerLeftPt.x)
               lowerLeftPt.x = other.lowerLeftPt.x;
            if (other.lowerLeftPt.y < lowerLeftPt.y)
               lowerLeftPt.y = other.lowerLeftPt.y;
            if (other.lowerLeftPt.z < lowerLeftPt.z)
               lowerLeftPt.z = other.lowerLeftPt.z;

            if (other.upperRightPt.x > upperRightPt.x)
               upperRightPt.x = other.upperRightPt.x;
            if (other.upperRightPt.y > upperRightPt.y)
               upperRightPt.y = other.upperRightPt.y;
            if (other.upperRightPt.z > upperRightPt.z)
               upperRightPt.z = other.upperRightPt.z;
         }
      }

      public void expandByPoint(Point aPoint)
      {
         expandByPoint(aPoint.x, aPoint.y, aPoint.z);
      }

      public void expandByPoint(Double x, Double y, Double? z)
      {
         if (this.isInitialized == false)
         {
            lowerLeftPt = new Point(x, y, z);
            upperRightPt = new Point(x, y, z);
            this.isInitialized = true;
         }
         else
         {
            if (x < lowerLeftPt.x)
               lowerLeftPt.x = x;
            if (y < lowerLeftPt.y)
               lowerLeftPt.y = y;
            if (z < lowerLeftPt.z)
               lowerLeftPt.z = z;

            if (x > upperRightPt.x)
               upperRightPt.x = x;
            if (y > upperRightPt.y)
               upperRightPt.y = y;
            if (z > upperRightPt.z)
               upperRightPt.z = z;
         }
      }

      public bool isPointInsideBB2d(Double x, Double y)
      {
         if (x < lowerLeftPt.x)
            return false;
         if (y < lowerLeftPt.y)
            return false;

         if (x > upperRightPt.x)
            return false;
         if (y > upperRightPt.y)
            return false;

         return true;
      }

      public bool isPointInsideBB2d(Point testPoint)
      {
         if (testPoint.x < lowerLeftPt.x)
            return false;
         if (testPoint.y < lowerLeftPt.y)
            return false;

         if (testPoint.x > upperRightPt.x)
            return false;
         if (testPoint.y > upperRightPt.y)
            return false;

         return true;
      }

      public bool isPointInsideBB3d(Point testPoint)
      {
         if (isPointInsideBB2d(testPoint) == false)
            return false;

         if (testPoint.z < lowerLeftPt.z)
            return false;

         if (testPoint.z > upperRightPt.z)
            return false;

         return true;
      }
   }
}
