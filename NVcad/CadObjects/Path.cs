using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Angles;
using NVcad.Foundations.Coordinates;

[assembly: InternalsVisibleTo("UnitTestNVcad")]
namespace NVcad.CadObjects
{
   public abstract class Path : Geometric
   {
      public Path() : base() { }

      public Point StartPoint // wrapper of base.base.Origin
      {
         get { return Origin; }
         set { Origin = value; }
      }
      public virtual Point EndPoint { get; set; }
      public virtual Azimuth StartAzimuth // adapter/wrapper of base.base.Rotation
      {
         get { return Azimuth.CreateFromCadConventionAngle(base.Rotation); }
         set 
         { 
            base.Rotation = value.GetAsCadConventionAngle();
            endAzimuth_ = Azimuth.CreateFromCadConventionAngle(base.Rotation) + deflection_;
         }
      }

      private Azimuth endAzimuth_;
      public virtual Azimuth EndAzimuth 
      {
         get { return endAzimuth_; }
         set
         {
            endAzimuth_ = value;
            deflection_ = endAzimuth_ - StartAzimuth;
         }
      }

      private Deflection deflection_;
      public virtual Deflection Deflection 
      {
         get { return deflection_; }
         set
         {
            deflection_ = value;
            endAzimuth_ = StartAzimuth + deflection_;
         }
      }

      public virtual Double Length { 
         get
         {
            return (this.EndPoint - this.Origin).Length;
         }
         }
   }
}
