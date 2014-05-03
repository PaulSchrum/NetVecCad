using NVcad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using NVcad;
using NVcad.CadObjects;
using NVCO = NVcad.CadObjects;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Primitives;
using NVcadView;

namespace NVcadModerator
{
   /// <summary>
   /// The Moderator Class is a key component of the Model-View-Moderator Pattern,
   /// a new pattern (invented by Paul Schrum in April 2014) that is in the same
   /// family as Model-View-ViewModel and Model-View-Controller.  
   /// </summary>
   /// <see cref=""/>
   public class Moderator : ICadNotificationTarget
   {
      protected Window myParentWindow { get; set; }
      WindowContainer cadViews { get; set; }
      //protected List<Window> childCadViews { get; set; }
      protected NVcad2dViewCanvas theCanvas { get; set; }
      protected TransformGroup xformGroup { get; set; }

      // public List<ViewWindow> allViewWindows = new List<ViewWindow>();
      private Model Model { get; set; }

      public Moderator()
      {
         Model = new Model(this);
      }

      public Moderator(NVcad2dViewCanvas aCanvas)
         : this()
      {
         this.theCanvas = aCanvas;
         xformGroup = new TransformGroup();
         xformGroup.Children.Add(new ScaleTransform(1.0, -1.0, 0.0, 0.0));
         xformGroup.Children.Add(new TranslateTransform(this.theCanvas.ActualWidth / 2.0, 1.0 * this.theCanvas.ActualHeight / 2.0));

         Model.setUpTestingModel_20140422();
      }

      public Moderator(Window aParentWindow, Xceed.Wpf.Toolkit.Primitives.WindowContainer aWindowContainer)
         : this()
      {
         myParentWindow = aParentWindow;

         cadViews = aWindowContainer;
         this.theCanvas = (cadViews.AddChildWindow()).primaryCanvas;
      }

      public void tempSeeIfYouCanDrawSomem()
      {
         xformGroup = new TransformGroup();
         xformGroup.Children.Add(new ScaleTransform(1.0, -1.0, 0.0, 0.0));
         xformGroup.Children.Add(new TranslateTransform(this.theCanvas.ActualWidth / 2.0, 1.0 * this.theCanvas.ActualHeight / 2.0));

         Model.setUpTestingModel_20140422();
      }

      public void DrawGraphicItem(Graphic graphicItem)
      {
         int i = 0;
         i++;
         if (graphicItem is NVCO.LineSegment)
         {
            DrawGraphicItem(graphicItem as NVCO.LineSegment);
         }
      }

      protected void DrawGraphicItem(NVCO.LineSegment lineSegment)
      {
         Line aLine = new Line();
         aLine.X1 = lineSegment.Origin.x;
         aLine.Y1 = lineSegment.Origin.y;
         aLine.X2 = lineSegment.EndPoint.x;
         aLine.Y2 = lineSegment.EndPoint.y;
         aLine.HorizontalAlignment = HorizontalAlignment.Left;
         aLine.VerticalAlignment = VerticalAlignment.Bottom;
         aLine.Stroke = Brushes.Black;
         aLine.StrokeThickness = 2.5;
         // aLine.Stroke = Stroke_;
         // aLine.StrokeDashArray = strokeDashArray_;
         aLine.RenderTransform = xformGroup;

         this.theCanvas.Children.Add(aLine);
      }

   }

   public static class ModeratorExtensionMethods
   {
      public static NVcad2dViewWindow AddChildWindow(this WindowContainer container)
      {
         NVcad2dViewWindow newWindow = new NVcad2dViewWindow();
         container.Children.Add(newWindow);
         newWindow.initializeCustomSettings();
         newWindow.Show();
         return newWindow;
      }
   }
}
