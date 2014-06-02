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
using NVCFND = NVcad.Foundations.Coordinates;
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
      protected TransformGroup xformGroup_all { get; set; }
      protected TransformGroup xformGroup_text1 { get; set; }
      protected TransformGroup xformGroup_text2 { get; set; }

      // public List<ViewWindow> allViewWindows = new List<ViewWindow>();
      private Model Model { get; set; }

      public Moderator()
      {
         Model = new Model(this);
      }

      public Moderator(
         Window aParentWindow,
         Xceed.Wpf.Toolkit.Primitives.WindowContainer aWindowContainer)
         : this()
      {
         myParentWindow = aParentWindow;

         cadViews = aWindowContainer;
      }

      public void CreateNewEmptyModel()
      {
         if (null == cadViews) return;
         // todo: offer to save the existing open model if necessary
         cadViews.Children.Clear();
         this.Model = new Model(this);
         Model.setUpTestingModel_20140422();
         this.refreshAllViews();
      }

      private void refreshAllViews()
      {
         foreach (NVcad2dViewWindow view in this.cadViews.Children)
         {
            view.refresh();
         }
      }

      public void ViewPortAdded(CadViewPort newViewPort)
      {  // Code Documentation Tag 20140530_02
         if (null == cadViews) return;  // todo: throw exception
         cadViews.AddChildWindow(newViewPort);
      }

      public void DrawGraphicItem(Graphic graphicItem)
      {
         //int i = 0;
         //i++;
         //if (graphicItem is NVCO.LineSegment)
         //{
         //   DrawGraphicItem(graphicItem as NVCO.LineSegment);
         //}
         //else if (graphicItem is NVCO.Text)
         //{
         //   DrawGraphicItem(graphicItem as NVCO.Text);
         //}
      }

      protected void DrawGraphicItem(NVCO.Text textItem)
      {
         var aTextBox = new TextBox();
         aTextBox.FontFamily = new FontFamily("Arial");
         aTextBox.FontSize = 24;
         aTextBox.BorderBrush = Brushes.Transparent;
         aTextBox.Background = Brushes.Transparent;
         aTextBox.Margin = new Thickness(0, 0, 0, 0);
         aTextBox.BorderThickness = new Thickness(1, 1, 1, 1);
         aTextBox.Padding = new Thickness(-6, -6, -6, -6);
         var tmp = textItem.Origin;
         aTextBox.Text = textItem.Content;
         if(textItem.Rotation.getAsDegreesDouble() != 0.0)
         {
            aTextBox.RenderTransform = xformGroup_text1;
            //var xformedOrigin = xformGroup_all.Transform(textItem.Origin);
            //var localXform = new TransformGroup(); //xformGroup_text1.Clone();
            //localXform.Children.Add(new RotateTransform(-1.0 * textItem.Rotation.getAsDegreesDouble(),
            //      textItem.Origin.x, textItem.Origin.y));
            ////localXform.Children.Add(new TranslateTransform(textItem.Origin.x, textItem.Origin.y));
            //foreach (var xform in xformGroup_text1.Children)
            //{
            //   localXform.Children.Add(xform);
            //}
            //aTextBox.RenderTransform = localXform;
            Canvas.SetLeft(aTextBox, textItem.Origin.x);
            Canvas.SetTop(aTextBox, textItem.Origin.y);// * xformGroup_text2.Value.M22);
         }
         else 
         { 
            aTextBox.RenderTransform = xformGroup_text1;
            Canvas.SetLeft(aTextBox, textItem.Origin.x);
            Canvas.SetTop(aTextBox, textItem.Origin.y * xformGroup_text2.Value.M22);
         }

         this.theCanvas.Children.Add(aTextBox);
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
         aLine.RenderTransform = xformGroup_all;

         this.theCanvas.Children.Add(aLine);
      }

      private void setUpTransforms()
      {
         xformGroup_all = new TransformGroup();
         xformGroup_all.Children.Add(new ScaleTransform(1.0, -1.0, 0.0, 0.0));
         xformGroup_all.Children.Add(
            new TranslateTransform(this.theCanvas.ActualWidth / 2.0,
               1.0 * this.theCanvas.ActualHeight / 2.0));

         xformGroup_text1 = new TransformGroup();
         //xformGroup_text.Children.Add(new ScaleTransform(1.0, 1.0));
         xformGroup_text1.Children.Add(
            new TranslateTransform(this.theCanvas.ActualWidth / 2.0,
               1.0 * this.theCanvas.ActualHeight / 2.0));

         xformGroup_text2 = new TransformGroup();
         xformGroup_text2.Children.Add(
            new ScaleTransform(1.0, -1.0));
         var hmm = xformGroup_text2.Value;
         var ox = hmm.OffsetX;
         var oy = hmm.OffsetY;
         var M11 = hmm.M11;
         var M12 = hmm.M12;
         var M21 = hmm.M21;
         var M22 = hmm.M22;
      }

   }

   public static class ModeratorExtensionMethods
   {
      public static NVcad2dViewWindow AddChildWindow
         (this WindowContainer container,
         CadViewPort newViewPort)
      {  // Code Documentation Tag 20140530_03
         NVcad2dViewWindow newWindow = new NVcad2dViewWindow(newViewPort);
         container.Children.Add(newWindow);
         newWindow.Show();
         return newWindow;
      }

      public static NVCFND.Point Transform(this TransformGroup xfrm, NVCFND.Point inPoint)
      {
         Point intermediatePoint = new Point(inPoint.x, inPoint.y);
         var nxtIntPt = xfrm.Transform(intermediatePoint);
         return new NVCFND.Point(nxtIntPt.X, nxtIntPt.Y);
      }

   }
}
