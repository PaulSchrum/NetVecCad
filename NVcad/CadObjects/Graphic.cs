using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Coordinates;
using NVcad.Foundations;
using NVcad.Foundations.Symbology;

namespace NVcad.CadObjects
{
   public abstract class Graphic : CadObject, IBoundingBoxed
   {
      public Feature Feature { get; set; }
      public Feature FeatureOverride { get; set; }
      public Point Origin { get; set; }

      protected Angle rotation_;
      public virtual Angle Rotation
      {
         get
         { return rotation_; }
         set
         { throw new NotSupportedException(); }
      }

      protected Double scale_;
      public virtual Double Scale
      {
         get
         { return scale_; }
         set
         { scale_ = value; }
      }

      public BoundingBox BoundingBox { get; set; }

      public BoundingBox getBoundingBox()
      { return this.BoundingBox; }

   }
}
