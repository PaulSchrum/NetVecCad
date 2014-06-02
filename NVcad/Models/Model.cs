using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NVcad.Foundations.Coordinates;
using NVcad.CadObjects;
using NVcad.Foundations;
using NVcad.Foundations.WorkingUnits;

namespace NVcad.Models
{
   public class Model : IBoundingBoxed
   {
      public Length WorkingUnits { get; set; }
      protected BoundingBox BoundingBox { get; set; }
      internal Dictionary<String, CadViewPort> allViewPorts { get; set; }
      internal List<Graphic> allGrahics { get; set; } // All except ViewPorts  // maybe refactor later to concurrent collection
      protected ICadNotificationTarget NotificationTarget { get; set; }

      public Model() 
      {
         BoundingBox = new BoundingBox();
         allViewPorts = new Dictionary<String, CadViewPort>();
         allGrahics = new List<Graphic>();
         NotificationTarget = null;
      }

      public Model(ICadNotificationTarget aNotificationTarget) : this()
      {
         this.NotificationTarget = aNotificationTarget;
         String defaultViewPortName = "1";
         // Code Documentation Tag 20140530_01
         var defaultViewPort = new CadViewPort(defaultViewPortName, this);
         this.AddViewPort(defaultViewPortName, defaultViewPort);

         // Code Documentation Tag 20140530_02
         aNotificationTarget.ViewPortAdded(defaultViewPort);
      }

      public BoundingBox getBoundingBox()
      {
         return BoundingBox;
      }

      internal void AddGraphic(Graphic newGraphic)
      {
         if (newGraphic is CadViewPort)
         { throw new InvalidGraphicTypeException(newGraphic); }

         this.BoundingBox.expandByBox(newGraphic.BoundingBox);
         allGrahics.Add(newGraphic);
      }

      public bool IsViewPortNameAvailable(String Name)
      {
         return !(allViewPorts.ContainsKey(Name));
      }

      internal void AddViewPort(String Name, CadViewPort newViewPort)
      {
         if (!(newViewPort is CadViewPort))
         { throw new InvalidGraphicTypeException(newViewPort); }

         if (allViewPorts.ContainsKey(Name) == true)
         { throw new ViewNameAlreadyExists(Name); }

         // Note: we do not expand this.BoundingBox for views.
         //    Very Important.
         allViewPorts.Add(Name, newViewPort);
      }

      public void setUpTestingModel_20140422()
      {
         allGrahics = new List<Graphic>();
         this.AddGraphic(new LineSegment(0.0, 0.0, 1.0, 1.0));

         this.AddGraphic(new LineSegment(0.1, 0.0, -1.0, 1.0));
         this.AddGraphic(new LineSegment(0.1, 0.0, -1.3, 1.3));

         this.AddGraphic(new LineSegment(0.1, 0.0, -1.0, -1.0));
         this.AddGraphic(new LineSegment(0.1, 0.0, -1.3, -1.3));
         this.AddGraphic(new LineSegment(0.1, 0.0, -1.3, -0.7));

         this.AddGraphic(new LineSegment(0.1, 0.0, 1.0, -1.0));
         this.AddGraphic(new LineSegment(0.1, 0.0, 1.3, -1.3));
         this.AddGraphic(new LineSegment(0.1, 0.0, 1.3, -0.7));
         this.AddGraphic(new LineSegment(0.1, 0.0, 1.6, -1.6));
         //this.AddGraphic(new Text("190, 50", new Point(180.0, 50.0)));
         //this.AddGraphic(new Text("50, 0", new Point(50.0, 0.0)));
         //var rotText = new Text("Rotated 30°", new Point(0.0, 70.0));
         //rotText.Rotation = Angle.radiansFromDegree(44.01);
         //this.AddGraphic(rotText);
         //foreach (var item in allGrahics)
         //   this.NotificationTarget.DrawGraphicItem(item);
      }

      public CadViewPort createViewForTesting_20140507()
      {
         String newName = "1";
         if (!(this.IsViewPortNameAvailable(newName))) return null;
         var newVP = new CadViewPort("1", this, new Point(0, 0), 5, 5, 
            new Vector(1, 1, null), 0);
         this.AddViewPort("1", newVP);
         return newVP;
      }

   }

   public class InvalidGraphicTypeException : Exception
   {
      public InvalidGraphicTypeException()
      {
      }

      public InvalidGraphicTypeException(Graphic badType)
         : base("NVcad: Invalid Graphic Type: " + badType.GetType().Name)
      {
      }
   }

   public class ViewNameAlreadyExists : Exception
   {
      public ViewNameAlreadyExists(String Name)
         : base("NVcad: A view of name \"" + Name + "\" already exists.")
      {
      }
   }
}
