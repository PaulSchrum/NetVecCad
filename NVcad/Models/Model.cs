using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NVcad.Foundations.Coordinates;
using NVcad.CadObjects;

namespace NVcad.Models
{
   public class Model : IBoundingBoxed
   {
      protected BoundingBox BoundingBox { get; set; }
      protected List<Graphic> allGrahics { get; set; }  // maybe refactor later to concurrent collection
      protected ICadNotificationTarget NotificationTarget { get; set; }

      public Model() { }

      public Model(ICadNotificationTarget aNotificationTarget)
      {
         this.NotificationTarget = aNotificationTarget;
      }

      public BoundingBox getBoundingBox()
      {
         return BoundingBox;
      }

      public void setUpTestingModel_20140422()
      {
         allGrahics = new List<Graphic>();
         allGrahics.Add(new LineSegment(10.0, 5.0, 10.0, 10.0 + 2.5*96.0));
         allGrahics.Add(new LineSegment(0.0, 0.0, 2.5*96.0, 0.0));
         allGrahics.Add(new Text("190, 50", new Point(175.0, 50.0)));
         allGrahics.Add(new Text("50, 100", new Point(50.0, 100.0)));
         foreach (var item in allGrahics)
            this.NotificationTarget.DrawGraphicItem(item);
      }
   }
}
