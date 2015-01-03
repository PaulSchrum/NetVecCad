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

[assembly: InternalsVisibleTo("UnitTestNVcad")]
namespace NVcad.CadObjects
{
   public abstract class Graphic : CadObject, IBoundingBoxed
   {
      public Graphic() : base()
      {
         Feature = Feature.Factory_NewFeature();
      }

      public Feature Feature { get; set; }
      public Feature FeatureOverride { get; set; }
      public Point Origin { get; set; }
      public long myIndex { get; set; }

      protected Angle rotation_;
      public virtual Angle Rotation
      {
         get
         { return rotation_; }
         set
         { setRotation(value); }
      }
      protected virtual void setRotation(Angle newRotation)
      {
         rotation_ = newRotation;
      }

      protected Vector scale_;
      private string p1;
      private Point point;
      private double p2;
      public virtual Vector ScaleVector
      {
         get { return scale_; }
         set { scale_ = value; }
      }

      //public virtual ToolTip GetToolTip()
      //{
      //   var result = new ToolTip();
      //   result.Content = "Graphic Item";
      //   return result;
      //}

      //protected Double xscale_;
      //public virtual Double xScale
      //{
      //   get
      //   { return xscale_; }
      //   set
      //   { xscale_ = value; }
      //}

      protected virtual void computeBoundingBox()
      { }

      public BoundingBox BoundingBox { get; protected set; }

      public BoundingBox getBoundingBox()
      { return this.BoundingBox; }

   }
}
