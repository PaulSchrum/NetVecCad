using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Coordinates;
using NVcad.Foundations;
using NVcad.Foundations.Symbology;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using NVcad.Foundations.Angles;

[assembly: InternalsVisibleTo("UnitTestNVcad")]
namespace NVcad.CadObjects
{
   public abstract class Graphic : CadObject, IBoundingBoxed
   {
      public Graphic() : base()
      {
         Feature = Feature.Factory_NewFeature();
         Rotation = new Angle(0);
      }

      public Feature Feature { get; set; }  // Foundational, not derived
      public Feature FeatureOverride { get; set; }  // Foundational, not derived
      public Point Origin { get; set; }  // Foundational, not derived
      public long myIndex { get; set; }  // Foundational, not derived

      public virtual Angle Rotation { get; set; }  // Foundational, not derived

      public virtual Vector ScaleVector { get; set; }  // Foundational, not derived

      protected virtual void computeBoundingBox() { }

      public BoundingBox BoundingBox { get; protected set; }

      public BoundingBox getBoundingBox()
      { return this.BoundingBox; }

   }
}
