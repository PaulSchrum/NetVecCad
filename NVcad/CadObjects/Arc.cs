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
      protected Arc() 
      {
      }

      public Arc(Point centerPt, Point StartPt, Deflection defl)
         : this()
      {
         Eccentricity = 0.0;
         Origin = StartPt;
         Deflection = defl;
         CenterPt = centerPt;

         BeginRadiusVector = Origin - CenterPt;
         BeginPointAngle = Deflection / 2;
         CentralVector = BeginRadiusVector + BeginPointAngle;
         EndRadiusVector = BeginRadiusVector + Deflection;
         EndPoint = CenterPt + EndRadiusVector;
         computeBoundingBox();
      }

      public Arc(Point StartPt, Azimuth incomingDir, Deflection defl,
         Double radius) : this()
      {
         Eccentricity = 0.0;
         Origin = StartPt;
         Deflection = defl;

         Azimuth BegRadiusDirection = incomingDir + 
            new Deflection(Math.PI / 2.0, -1 * Deflection.deflectionDirection); 

         BeginRadiusVector = new Vector(
            direction: BegRadiusDirection, 
            length: radius);
         CenterPt = StartPt - BeginRadiusVector;
         BeginPointAngle = Deflection / 2;
         CentralVector = BeginRadiusVector + BeginPointAngle;
         EndRadiusVector = BeginRadiusVector + Deflection;
         EndPoint = CenterPt + EndRadiusVector;

         computeBoundingBox();
      }

      public Point CenterPt { get; set; }
      public Vector BeginRadiusVector { get; protected set; }
      public Vector EndRadiusVector { get; protected set; }

      public override Angle Rotation
      {
         get
         {
            return BeginRadiusVector.DirectionHorizontal + 
               Angle.radiansFromDegree(this.deflection_.deflectionDirection * 90.0);
         }
         set
         { }
      }
      
      public override Vector ScaleVector
      {
         get {  return new Vector(1, 1, null); }
         set { }
      }

      protected override void computeBoundingBox()
      {
         this.BoundingBox = new BoundingBox(CenterPt);
         this.BoundingBox.expandByRadius(this.Radius);
      }
   }
}
