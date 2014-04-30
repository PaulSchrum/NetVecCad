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
         foreach (var item in allGrahics)
            this.NotificationTarget.DrawGraphicItem(item);
      }
   }
}
