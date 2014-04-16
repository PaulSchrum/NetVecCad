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
   public abstract class Graphic : CadObject
   {
      public Feature Feature { get; set; }
      public Feature FeatureOverride { get; set; }
      public Point Origin { get; set; }
      public Angle Rotation { get; set; }
      public Double Scale { get; set; }
      public BoundingBox BoundingBox { get; set; }
   }
}
