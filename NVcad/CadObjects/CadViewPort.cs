using NVcad.Foundations;
using NVcad.Foundations.Coordinates;
using NVcad.Models;
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
      // base.Origin is the view center.
      // base.Rotation is the view rotation about the Z-axis only.
      public Double Height { get { return ViewAspect.y; } protected set { } }
      public Double Width { get { return ViewAspect.x; } protected set { } }
      public Double? Depth { get { return ViewAspect.z; } protected set { } }
      public Vector ViewAspect { get; set; }  // see comments under base.Scale, below.
      
      // base.ScaleVector 
      //            is the view scale vector.  This is true screen scale,
      // based on WPF's 96 pixels per screen inch.  Thus,
      // 1-scale is 1 world Primary World Unit per screen inch.  So when 
      // World Unit System is English, 1 foot = 1 screen inch when 
      // ScaleVector is [1, 1].
      // Scale and ViewAspect are independent of each other.

      public NVcad.Models.Model parentModel { get; set; }

      private ICadViewChangedNotification pairedUIview_;
      public ICadViewChangedNotification pairedUIview
      {
         get { return pairedUIview_; }
         set
         {
            pairedUIview_ = value;
            //pairedUIview_.ViewCreatedAnew();
         }
      }

      public CadViewPort(String name, Model parentMdl)
      {
         this.parentModel = parentMdl;
         Name = name;
         ViewAspect = new Vector(3, 3, null);
         Origin = new Point(0,0);
         ScaleVector = new Vector(1.0, 1.0, 1.0);
         Rotation = new Angle(0);
         updateBoundingBox();
      }

      public CadViewPort(String name, CadViewPort other) : this(name, other.parentModel)
      {
         Height = other.Height; Width = other.Width;
         Origin = new Point(other.Origin);
         ScaleVector = new Vector(other.scale_.x, other.scale_.y, other.scale_.z);
         Rotation = new Angle(other.Rotation.angle_);
      }

      public CadViewPort(String name, Model parentMdl,
         Point center, Vector aspectVec,
         Vector scaleVec,
         Angle rotation)
         : this(name, parentMdl)
      {
         Origin = center;
         ViewAspect = aspectVec;
         ScaleVector = scaleVec;
         Rotation = rotation;
      }

      public CadViewPort(String name, Model parentMdl,
         Point center, Double height, Double width,
         Vector scaleVec,
         Angle rotation)
         : this(name, parentMdl, center, new Vector(width, height, null), scaleVec, rotation)
      { }

      public IEnumerable<Graphic> getVisibleGraphicElements()
      {
         var visibleGraphicItems = 
            from grphic in parentModel.allGrahics.AsParallel()
            where this.BoundingBox.overlapsWith(grphic.BoundingBox)
            select grphic;
         //var visibleViews = 
         //   from visView in parentModel.allViewPorts
         //   where ((this.BoundingBox.overlapsWith(visView.Value.BoundingBox)) && 
         //      (visView.Key != this.Name))
         //   select visView.Value;
         //var allOfEm = visibleGraphicItems.Union(visibleViews);
         var sortedByDisplayPriority = 
            from item in visibleGraphicItems.AsParallel()
            orderby item.Feature.DisplayPriority
            select item;

         return sortedByDisplayPriority;
      }

      public void ScaleAbout(Point scalePt, Double scaleFactor)
      {
         this.scale_.x *= scaleFactor;
         this.scale_.y *= scaleFactor;
         this.scale_.z *= scaleFactor;

         this.Height *= scaleFactor;
         this.Width *= scaleFactor;

         this.Origin.x = scalePt.x - (scaleFactor * (scalePt.x - this.Origin.x));
         this.Origin.y = scalePt.y - (scaleFactor * (scalePt.y - this.Origin.y));
         this.Origin.z = scalePt.z - (scaleFactor * (scalePt.z - this.Origin.z));

         pairedUIview.ViewGeometryChanged();
      }

      protected void updateBoundingBox()
      {
         if (null == this.BoundingBox) this.BoundingBox = new BoundingBox();
         this.BoundingBox.setFrom_2dOnly(this.Origin,
            this.Width / (this.ScaleVector.x * 96),
            this.Height / (this.ScaleVector.y * 96),
            this.Rotation);
      }

      public void setCadView(ICadViewChangedNotification correspondingUIview)
      {
         pairedUIview = correspondingUIview;
      }

      public void SetHeightAndWidth(Double height, Double width, Boolean triggerRedraw)
      {
         this.ViewAspect = new Vector(height, width, null);
         updateBoundingBox();
         if(true == triggerRedraw)
            this.pairedUIview.ViewContentsChanged();
      }


      public void SlideOrigin(double dx, double dy, Double? dz)
      {
         this.Origin.x += dx;
         this.Origin.y += dy;
         if (null != dz && null != this.Origin.z) this.Origin.z += dz;
         // todo: update for rotated views

         this.BoundingBox.Slide(dx, dy, dz);
      }

      internal void FitView()
      {
         Vector screenSizeVec = new Vector();
         screenSizeVec.x = pairedUIview.getWidth() / 96;
         screenSizeVec.y = pairedUIview.getHeight() / 96;

         Vector possibleScales = new Vector();
         Vector parentBBvec = this.parentModel.getBoundingBox().getAsVectorLLtoUR();
         possibleScales.x = parentBBvec.x / screenSizeVec.x;
         possibleScales.y = parentBBvec.y / screenSizeVec.y;

         Double scaleToUse = possibleScales.getAbsMax();

         Double verticalExaggeration = this.scale_.y /
            this.scale_.x;
         this.scale_.x = scaleToUse;
         this.scale_.y = this.scale_.x * verticalExaggeration;
         //this.scale_.z = screenVec.x;

         this.Origin = this.parentModel.getBoundingBox().getCenterPoint();
         this.updateBoundingBox();

         pairedUIview.ViewGeometryChanged();
      }
   }
}
