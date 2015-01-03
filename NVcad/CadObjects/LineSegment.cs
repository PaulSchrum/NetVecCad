using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NVcad.Foundations;
using NVcad.Foundations.Coordinates;


[assembly: InternalsVisibleTo("UnitTestNVcad")]
namespace NVcad.CadObjects
{
   public class LineSegment : Path
   {
      protected LineSegment() : base()
      { }

      public LineSegment(Double X1, Double Y1, Double X2, Double Y2)
         : this()
      {
         this.Origin = new Point(X1, Y1);
         this.EndPoint = new Point(X2, Y2);
         this.BoundingBox = new BoundingBox(this.Origin);
         this.BoundingBox.expandByPoint(this.EndPoint);
      }

      public LineSegment(netDxf.Entities.Line dxfLine)
      {
         this.Origin = (Point) dxfLine.StartPoint;
         this.EndPoint = (Point)dxfLine.EndPoint;
         this.BoundingBox = new BoundingBox(this.Origin);
         this.BoundingBox.expandByPoint(this.EndPoint);
      }

      public override Angle Rotation
      {
         get
         {
            return new Angle(this.Origin, this.EndPoint);
         }
         set
         { }
      }

      public override Vector ScaleVector
      {
         get
         {  return new Vector(1, 1, 1); }
         set
         { }
      }

      //public override ToolTip GetToolTip()
      //{
      //   var result = base.GetToolTip();
      //   StringBuilder sb = new StringBuilder("Type: LineSegment\n");
      //   sb.Append("Feature: ");
      //   sb.Append(this.Feature.Name);
      //   sb.Append("\n");
      //   sb.Append("Length: ");
      //   sb.Append(this.Length.ToString("F4", CultureInfo.InvariantCulture));
      //   result.Content = sb.ToString();
      //   return result;
      //}

   }
}
