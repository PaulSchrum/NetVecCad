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
   public abstract class ConicSegment : Path
   {
      protected Double eccentricity_;
      public virtual Double Eccentricity 
      {
         get { return eccentricity_; }
         set { eccentricity_ = value; }
      }

      public virtual Double Radius
      {
         get { return centralVector_.Length; }
         set { centralVector_.Length = value; }
      }

      protected Vector centralVector_;
      public virtual Vector CentralVector
      {
         get { return centralVector_; }
         set { centralVector_ = value; }
      }

      protected Deflection deflection_;
      public virtual Deflection Deflection
      {
         get { return deflection_; }
         set { deflection_ = value; }
      }

      /// <summary>
      /// Begin Point Angle is the deflection from the central
      ///    vector to the begin vector
      /// </summary>
      protected Deflection beginPointAngle_;
      public virtual Deflection BeginPointAngle
      {
         get { return beginPointAngle_; }
         set { beginPointAngle_ = value; }
      }
   }
}
