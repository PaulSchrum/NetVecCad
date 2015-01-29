using NVcad.Foundations;
using NVcad.Foundations.Angles;
using NVcad.Foundations.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTestNVcad")]
namespace NVcad.CadObjects
{
   public abstract class ConicSegment : Path
   {
      public ConicSegment() : base() { }

      public virtual Double Eccentricity 
      {
         get { return 0.0; }
         protected set { }
      }

      public virtual Double Radius
      {
         get; 
         //{ return centralVector_.Length; }
         set; 
         //{ centralVector_.Length = value; }
      }

      public virtual Double a { get; set; }

      public virtual Double b { get; set; }
      
      //protected Vector centralVector_;
      //public virtual Vector CentralVector
      //{
      //   get { return centralVector_; }
      //   set { centralVector_ = value; }
      //}

      //protected Deflection deflection_;
      //public virtual Deflection Deflection
      //{
      //   get { return deflection_; }
      //   set { deflection_ = value; }
      //}

      ///// <summary>
      ///// Begin Point Angle is the deflection from the central
      /////    vector to the begin vector
      ///// </summary>
      //protected Deflection beginPointAngle_;
      //public virtual Deflection BeginPointAngle
      //{
      //   get { return beginPointAngle_; }
      //   set { beginPointAngle_ = value; }
      //}
   }
}
