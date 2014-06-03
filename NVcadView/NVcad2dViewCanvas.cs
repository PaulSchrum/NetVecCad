using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NVcad;
using NVcad.CadObjects;
using NVCO = NVcad.CadObjects;
using NVCFND = NVcad.Foundations.Coordinates;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Primitives;
using NVcadView;

namespace NVcadView
{
   /// <summary>
   /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
   ///
   /// Step 1a) Using this custom control in a XAML file that exists in the current project.
   /// Add this XmlNamespace attribute to the root element of the markup file where it is 
   /// to be used:
   ///
   ///     xmlns:MyNamespace="clr-namespace:NVcadView"
   ///
   ///
   /// Step 1b) Using this custom control in a XAML file that exists in a different project.
   /// Add this XmlNamespace attribute to the root element of the markup file where it is 
   /// to be used:
   ///
   ///     xmlns:MyNamespace="clr-namespace:NVcadView;assembly=NVcadView"
   ///
   /// You will also need to add a project reference from the project where the XAML file lives
   /// to this project and Rebuild to avoid compilation errors:
   ///
   ///     Right click on the target project in the Solution Explorer and
   ///     "Add Reference"->"Projects"->[Browse to and select this project]
   ///
   ///
   /// Step 2)
   /// Go ahead and use your control in the XAML file.
   ///
   ///     <MyNamespace:CustomControl1/>
   ///
   /// </summary>
   public class NVcad2dViewCanvas : Canvas, ICadViewChangedNotification
   {
      CadViewPort myCadViewPort_;
      internal CadViewPort myCadViewPort
      {
         get { return myCadViewPort_; }
         set 
         { 
            myCadViewPort_ = value;
            if (null != myCadViewPort_) myCadViewPort_.pairedUIview = this;
            //establishTransforms();
         }
      }

      public NVcad2dViewCanvas() : base()
      {
         this.ClipToBounds = true;
         myCadViewPort = null;
      }

      public NVcad2dViewCanvas(CadViewPort newViewPort) : this()
      {
         myCadViewPort = newViewPort;
         myCadViewPort.pairedUIview = this;
      }

      Point canvasCenter;
      Double itemWidthUnscale = 0;
      TransformGroup xformGroup_allButText;
      TransformGroup xformGroup_text1;
      TransformGroup xformGroup_text2;
      internal void establishTransforms()
      {
         if (this.ActualWidth <= 0.0) return;
         this.myCadViewPort.SetHeightAndWidth(this.ActualHeight, this.ActualWidth, false);
         canvasCenter.X = this.ActualWidth / 2.0;
         canvasCenter.Y = this.ActualHeight / 2.0;
         itemWidthUnscale = 1.0 / 96.0;
         xformGroup_allButText = new TransformGroup();
         xformGroup_allButText.Children.Add(
            new ScaleTransform(1, -1)
            );
         var w = this.ActualWidth;
         var h = this.ActualHeight;
         xformGroup_allButText.Children.Add(
            new TranslateTransform(canvasCenter.X, canvasCenter.Y));
         xformGroup_allButText.Children.Add(
            new ScaleTransform(
               96.0 / this.myCadViewPort.ScaleVector.x, 
               96.0 / this.myCadViewPort.ScaleVector.y, 
               canvasCenter.X, canvasCenter.Y)
            );
      }

      public void ViewCreatedAnew()
      {
         //this.establishTransforms();
         var visibleObjects = myCadViewPort.getVisibleGraphicElements();
         if (null == visibleObjects) return;
         foreach (var graphic in visibleObjects)
         {
            DrawGraphicItem(graphic);
         }
      }

      public void DrawGraphicItem(Graphic graphicItem)
      {
         int i = 0;
         i++;
         if (graphicItem is NVCO.LineSegment)
         {
            DrawGraphicItem(graphicItem as NVCO.LineSegment);
         }
         else if (graphicItem is NVCO.Text)
         {
            DrawGraphicItem(graphicItem as NVCO.Text);
         }
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
         if (textItem.Rotation.getAsDegreesDouble() != 0.0)
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

         this.Children.Add(aTextBox);
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
         aLine.StrokeThickness = 2.5 * itemWidthUnscale;
         // aLine.Stroke = Stroke_;
         // aLine.StrokeDashArray = strokeDashArray_;
         aLine.RenderTransform = xformGroup_allButText;

         this.Children.Add(aLine);
      }

      //static NVcad2dViewCanvas()
      //{
      //   DefaultStyleKeyProperty.OverrideMetadata(typeof(NVcad2dViewCanvas), 
      //        new FrameworkPropertyMetadata(typeof(NVcad2dViewCanvas)));
      //}

      public void ViewContentsChanged()
      {
         this.refresh();
      }

      internal void refresh()
      {
         foreach (var grphic in this.myCadViewPort.getVisibleGraphicElements())
         {
            this.DrawGraphicItem(grphic);
         }
      }
   }
}
