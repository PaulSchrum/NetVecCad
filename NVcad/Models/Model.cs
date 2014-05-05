using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NVcad.Foundations.Coordinates;
using NVcad.CadObjects;
using NVcad.Foundations;

namespace NVcad.Models
{
   public class Model : IBoundingBoxed
   {
      protected BoundingBox BoundingBox { get; set; }
      protected List<Graphic> allGrahics { get; set; } // All except ViewPorts  // maybe refactor later to concurrent collection
      protected ICadNotificationTarget NotificationTarget { get; set; }

      public Model() 
      {
         BoundingBox = new BoundingBox();
         allGrahics = new List<Graphic>();
         NotificationTarget = null;
      }

      public Model(ICadNotificationTarget aNotificationTarget) : this()
      {
         this.NotificationTarget = aNotificationTarget;
      }

      public BoundingBox getBoundingBox()
      {
         return BoundingBox;
      }

      protected void AddGraphic(Graphic newGraphic)
      {
         this.BoundingBox.expandByBox(newGraphic.BoundingBox);
         allGrahics.Add(newGraphic);
      }

      public void setUpTestingModel_20140422()
      {
         allGrahics = new List<Graphic>();
         this.AddGraphic(new LineSegment(0.0, 0.0, 0.0, 0.0 + 2.5 * 96.0));
         //allGrahics.Add(new LineSegment(0.0, 0.0, 0.0, 0.0 + 2.5*96.0));
         this.AddGraphic(new LineSegment(0.0, 0.0, 2.5 * 96.0, 0.0));
         this.AddGraphic(new Text("190, 50", new Point(180.0, 50.0)));
         this.AddGraphic(new Text("50, 0", new Point(50.0, 0.0)));
         var rotText = new Text("Rotated 30°", new Point(0.0, 70.0));
         rotText.Rotation = Angle.radiansFromDegree(180.0);
         //this.AddGraphic(rotText);
         foreach (var item in allGrahics)
            this.NotificationTarget.DrawGraphicItem(item);
      }
   }
}
