using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFmedia=System.Windows.Media;

using NVcad.Foundations.Coordinates;
using NVcad.CadObjects;
using NVcad.Foundations;
using NVcad.Foundations.WorkingUnits;
using System.ComponentModel;
using NVcad.Foundations.Angles;
using NVcad.Foundations.Symbology;
using NVcad.Foundations.Symbology.Color;

namespace NVcad.Models
{
   public class Model : IBoundingBoxed, INotifyPropertyChanged
   {
      public Length WorkingUnits { get; set; }
      protected BoundingBox BoundingBox { get; set; }
      public WorldMouse WorldMouse { get; protected set; }
      internal Dictionary<String, CadViewPort> allViewPorts { get; set; }
      internal List<Graphic> allGrahics { get; set; } // All except ViewPorts  // maybe refactor later to concurrent collection
      protected ICadNotificationTarget NotificationTarget { get; set; }
      public FeatureList FeatureList { get; protected set; }

      public Model() 
      {
         WorldMouse = new Models.WorldMouse();
         BoundingBox = new BoundingBox();
         allViewPorts = new Dictionary<String, CadViewPort>();
         allGrahics = new List<Graphic>();
         NotificationTarget = null;
         FeatureList = new FeatureList();
      }

      public Model(ICadNotificationTarget aNotificationTarget) : this()
      {  // Code Documentation Tag 20140603_01
         this.NotificationTarget = aNotificationTarget;
         String defaultViewPortName = "1";

         // Code Documentation Tag 20140603_02
         var defaultViewPort = new CadViewPort(defaultViewPortName, this);
         defaultViewPort.Origin.x = 0.0;
         defaultViewPort.Origin.y = 0.0;
         this.AddViewPort(defaultViewPortName, defaultViewPort);

         // Code Documentation Tag 20140603_03
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

      public void fitView(String viewName)
      {  // Technical Debt: This only works on the first view.
         // Later it must work on the view that the command
         // issues from.
         CadViewPort aView;
         if (viewName.Length > 0)
         {
            if (!allViewPorts.ContainsKey(viewName)) return;
            aView = allViewPorts[viewName];
         }
         else
         {
            aView = allViewPorts.FirstOrDefault().Value;
         }
         aView.FitView();
      }

      public void setUpTestingModel_20140422()
      {
         setUpFeaturesForTestingModel();
         allGrahics = new List<Graphic>();
         this.AddGraphic(new LineSegment(1.0, 8.5, 13.2, 0.8));
         this.AddGraphic(new LineSegment(1.0, 2.5, 3.2, 0.8));
         this.AddGraphic(new LineSegment(0.0, 0.0, 0.5, 0.25));
         this.AddGraphic(new LineSegment(2.0, 1.0, 1.0, 1.0));

         this.AddGraphic(new LineSegment(0.0, 0.0, 1.5, 0.0));

         this.AddGraphic(new LineSegment(0.1, 0.0, -1.3, -1.3));
         this.AddGraphic(new LineSegment(0.1, 0.0, -1.3, -0.7));

         this.AddGraphic(new LineSegment(0.1, 0.0, 1.0, -1.0));
         this.AddGraphic(new LineSegment(0.1, 0.0, 1.3, -0.7));
         this.AddGraphic(new LineSegment(0.1, 0.0, 1.6, -1.6));
         this.AddGraphic(new Text("1.6, -0.6", new Point(1.6, -0.6), 0.25));
         this.AddGraphic(new Text("-2, +0.9", new Point(-2, 0.9), 0.45));

         var rotexPt = new Point(-1, -1);
         //var rotText = new Text("Rotated 1", rotexPt, 0.3);
         //rotText.Rotation = Angle.radiansFromDegree(1);
         //this.AddGraphic(rotText);

         //var rotText2 = new Text("Rotated 5", rotexPt, 0.3);
         //rotText2.Rotation = Angle.radiansFromDegree(5);
         //this.AddGraphic(rotText2);

         var rotText3 = new Text("Rotated 340", rotexPt, 0.3);
         rotText3.Rotation = Angle.radiansFromDegree(340);
         this.AddGraphic(rotText3);

         var anArc = new Arc(new Point(-1.4, 1.5),
            Azimuth.ctorAzimuthFromDegree(20.0),
            Deflection.ctorDeflectionFromAngle(350.0, 1), 0.25);
         anArc.Feature = this.FeatureList.Children["BlueDashed"];
         this.AddGraphic(anArc);
         allGrahics[0].Feature = this.FeatureList.Children["GreenDot"];
         allGrahics[1].Feature = this.FeatureList.Children["RedThick"];
         allGrahics[2].Feature = this.FeatureList.Children["BlueDashed"];
      }

      private void setUpFeaturesForTestingModel()
      {
         Feature ft = new Feature();
         ft.Name = "RedThick";
         ft.Color = (ColorAsBrush)WPFmedia.Brushes.Red;
         ft.Weight = 9;
         ft.Thickness = 1/12.0;
         ft.Style = 2;  // Medium Dashed
         this.FeatureList.AddFeature(ft);

         ft = new Feature();
         ft.Name = "BlueDashed";
         ft.Color = (ColorAsBrush)WPFmedia.Brushes.Blue;
         ft.Weight = 1;
         ft.Style = 1;  // Long Dashed
         this.FeatureList.AddFeature(ft);

         ft = new Feature();
         ft.Name = "GreenDot";
         ft.Color = (ColorAsBrush)WPFmedia.Brushes.Green;
         ft.Weight = 1;
         ft.Style = 3;  // Dots
         this.FeatureList.AddFeature(ft);

      }

      public event PropertyChangedEventHandler PropertyChanged;
      public void RaisePropertyChanged(String prop)
      {
         if (null != PropertyChanged)
         {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
         }
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
