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
      protected Canvas theCanvas { get; set; }
      protected TransformGroup xformGroup { get; set; }

      // public List<ViewWindow> allViewWindows = new List<ViewWindow>();
      private Model Model { get; set; }

      public Moderator()
      {
         Model = new Model(this);
      }

      public Moderator(Canvas aCanvas)
         : this()
      {
         this.theCanvas = aCanvas;
         xformGroup = new TransformGroup();
         xformGroup.Children.Add(new ScaleTransform(1.0, -1.0, 0.0, 0.0));
         xformGroup.Children.Add(new TranslateTransform(this.theCanvas.ActualWidth / 2.0, 1.0 * this.theCanvas.ActualHeight / 2.0));

         Model.setUpTestingModel_20140422();
      }

      public Moderator(Window aParentWindow, Xceed.Wpf.Toolkit.Primitives.WindowContainer aWindowContainer)
      {
         myParentWindow = aParentWindow;

         cadViews = aWindowContainer;
         NVcad2dViewWindow aWindow = new NVcad2dViewWindow();
         cadViews.Children.Add(aWindow);
         initializeChildCadView(aWindow);
         
      }

      protected void initializeChildCadView(NVcad2dViewWindow aWindow)
      {
         if (null == aWindow) return;

         aWindow.Height = 500;
         aWindow.Width = 550;
         aWindow.Left = 10;
         aWindow.Top = 10;
         aWindow.Caption = "NVCad 视图窗口";
         aWindow.IsModal = false;

         var contentGrid = new Grid();
         contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
         contentGrid.ColumnDefinitions[0].Width = new GridLength(20);
         contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
         contentGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
         contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
         contentGrid.ColumnDefinitions[2].Width = new GridLength(20);

         contentGrid.RowDefinitions.Add(new RowDefinition());
         contentGrid.RowDefinitions[0].Height = new GridLength(20);
         contentGrid.RowDefinitions.Add(new RowDefinition());
         contentGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
         contentGrid.RowDefinitions.Add(new RowDefinition());
         contentGrid.RowDefinitions[2].Height = new GridLength(20);

         var cvs = new Canvas();
         cvs.Background = Brushes.Bisque;
         contentGrid.Children.Add(cvs);
         Grid.SetRow(cvs, 1); Grid.SetColumn(cvs, 1);

         aWindow.Content = contentGrid;


         aWindow.Show();
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
}
