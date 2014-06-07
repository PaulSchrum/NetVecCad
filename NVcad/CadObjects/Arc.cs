using NVcad.Foundations;
using NVcad.Foundations.Angles;
using NVcad.Foundations.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVcad.CadObjects
{
   public class Arc : ConicSegment
   {
      protected Arc() { }

      public Arc(Point centerPt, Point StartPt, Deflection sweepAngle)
      {
         Eccentricity = 0.0;
         Origin = StartPt;
         SweepAngle = sweepAngle;
         CenterPt = centerPt;

         BeginRadiusVector = Origin - CenterPt;
         BeginPointAngle = SweepAngle / 2;
         CentralVector = BeginRadiusVector + BeginPointAngle;
         EndRadiusVector = BeginRadiusVector + SweepAngle;
         EndPoint = CenterPt + EndRadiusVector;
      }

      public Point CenterPt { get; set; }
      public Vector BeginRadiusVector { get; protected set; }
      public Vector EndRadiusVector { get; protected set; }

      public override Angle Rotation
      {
         get
         {
            return BeginRadiusVector.DirectionHorizontal + Angle.radiansFromDegree(90.0);
         }
         set
         { }
      }
      
      public override Vector ScaleVector
      {
         get {  return new Vector(1, 1, 1); }
         set { }
      }
   }
}
