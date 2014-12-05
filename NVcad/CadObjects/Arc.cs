using NVcad.Foundations;
using NVcad.Foundations.Angles;
using NVcad.Foundations.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTestNVcad")]
namespace NVcad.CadObjects
{
   public class Arc : ConicSegment
   {
      protected Arc() : base()
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
         populateThis(StartPt, incomingDir, defl, radius);
      }

      public Arc(Point centerPt, Azimuth incomingDir, Azimuth outgoingDir, Double radius)
      {
         int modifier = (outgoingDir - incomingDir) > 0 ? 1 : -1;
         Azimuth BegRadiusDirection = incomingDir +
            new Deflection(Math.PI / 2.0, -1 * modifier);
         BeginRadiusVector = new Vector(
            direction: BegRadiusDirection, 
            length: radius);
         Point startPt = centerPt + BeginRadiusVector;
         Deflection def = outgoingDir - incomingDir;
         populateThis(startPt, incomingDir, def, radius);
      }

      public Arc(netDxf.Entities.Arc dxfArc)
         : this
         ( (Point) dxfArc.Center
         , dxfArc.StartAngle.AsAzimuth()
         , dxfArc.EndAngle.AsAzimuth()
         , dxfArc.Radius)
      {

      }

      private void populateThis(Point StartPt, Azimuth incomingDir, Deflection defl,
         Double radius)
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

      public override ToolTip GetToolTip()
      {
         var result = base.GetToolTip();
         StringBuilder sb = new StringBuilder("Type: Arc\n");
         sb.Append("Feature: ");
         sb.Append(this.Feature.Name);
         sb.Append("\n");
         sb.Append("Length: ");
         sb.Append(this.Length.ToString("F4", CultureInfo.InvariantCulture));
         sb.Append("\n");
         sb.Append("Radius: ");
         sb.Append(this.Radius.ToString("F4", CultureInfo.InvariantCulture));
         sb.Append("\n");
         sb.Append("Deflection: ");
         sb.Append(this.Deflection.ToString());
         result.Content = sb.ToString();
         return result;
      }

      protected override void computeBoundingBox()
      {
         this.BoundingBox = new BoundingBox(CenterPt);
         this.BoundingBox.expandByRadius(this.Radius);
      }
   }
}
