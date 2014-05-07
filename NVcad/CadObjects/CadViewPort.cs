using NVcad.Foundations;
using NVcad.Foundations.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVcad.CadObjects
{
   public class CadViewPort : Graphic
   {
      public String Name { get; set; }
      public Double Height { get; set; }
      public Double Width { get; set; }
      // base.Origin is the view center.
      // base.Rotation is the view rotation.
      // base.Scale is the view scale vector.

      public CadViewPort(String name)
      {
         Name = name;
         Height = 100; Width = 100;
         Origin = new Point(0,0);
         ScaleVector = new Vector(1.0, 1.0, 1.0);
         Rotation = new Angle(0);
      }

      public CadViewPort(String name, CadViewPort other) : this(name)
      {
         Height = other.Height; Width = other.Width;
         Origin = new Point(other.Origin);
         ScaleVector = new Vector(other.scale_.x, other.scale_.y, other.scale_.z);
         Rotation = new Angle(other.Rotation.angle_);
      }

      public CadViewPort(String name, 
         Point center, Double height, Double width,
         Vector scaleVec,
         Angle rotation) : this(name)
      {
         Origin = center;
         Height = height; Width = width;
         ScaleVector = scaleVec;
         Rotation = rotation;
      }

      protected void updateBoundingBox()
      {
          
      }
   }
}
