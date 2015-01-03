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
using System.ComponentModel;
using System.Media;
using System.Windows.Threading;
using NVcad.Foundations.Symbology;
using SYMB = NVcad.Foundations.Symbology;

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
   public class NVcad2dViewCanvas : Canvas,
      ICadViewChangedNotification
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
         this.MouseMove +=NVcad2dViewCanvas_MouseMove;
         this.ClipToBounds = true;
         myCadViewPort = null;
      }

      public NVcad2dViewCanvas(CadViewPort newViewPort) : this()
      {  // Code Documentation Tag 20140603_05
         myCadViewPort = newViewPort;
         myCadViewPort.pairedUIview = this;
         this.MouseDown += new MouseButtonEventHandler(local_MouseDown);
         this.MouseMove += new MouseEventHandler(local_MouseMove);
         this.MouseWheel += new MouseWheelEventHandler(local_MouseWheel);
      }

      private double wheelZoomFactor = Math.Sqrt(2.0);
      private void local_MouseWheel(object sender, MouseWheelEventArgs e)
      {
         Double actualScale = wheelZoomFactor;
         int sign = Math.Sign(e.Delta);
         if (sign > 0) actualScale = 1.0 / wheelZoomFactor;

         Point screenPt = e.GetPosition(this);
         NVCFND.Point zoomPt = GetWorldPoint(e.GetPosition(this));
         this.myCadViewPort.ScaleAbout(zoomPt, actualScale);
      }

      private NVCFND.Point GetWorldPoint(Point screenPt)
      {
         var xfPoint = xformGroup_all.Inverse.Transform(screenPt);
         return
            (NVCFND.Point)xfPoint;
      }

      private void updateTransformsForScale(Double scale)
      {
         this.myCadViewPort.ScaleVector.scale(
            scale, scale, null);
         this.xformGroup_all.Children.Add(
               new ScaleTransform(scale, scale)
            );
         this.refresh();
      }

      System.Windows.Point mousePos_;
      public Point MousePos
      {
         get { return mousePos_; }
         protected set
         {
            mousePos_ = value;
            handleMouseMoves();
         }
      }

      protected virtual void handleMouseMoves()
      {
         try
         {
            myCadViewPort.parentModel.WorldMouse.PointX = mousePos_.X;
            myCadViewPort.parentModel.WorldMouse.PointY = mousePos_.Y;
         }
#pragma warning disable 0169
         catch(NullReferenceException)
         {
            return;
         }
#pragma warning restore 0169
      }

      private Point lastPos { get; set; }
      private Point moveDelta;
      private void local_MouseMove(object sender, MouseEventArgs e)
      {
         if(e.LeftButton == MouseButtonState.Pressed)
         {
            moveDelta.X = e.GetPosition(this).X - lastPos.X;
            moveDelta.Y = e.GetPosition(this).Y - lastPos.Y;
            updateTranformsForTranslate();
            lastPos = e.GetPosition(this);
         }
      }

      private void local_MouseDown(object sender, MouseEventArgs e)
      {
         lastPos = e.GetPosition(this);
      }

      Point canvasCenter;
      TransformGroup xformGroup_all;
      internal void establishTransforms()
      {  // Code Documentation Tag 20140603_06
         if (this.ActualWidth <= 0.0) return;
         this.myCadViewPort.SetHeightAndWidth(this.ActualHeight, this.ActualWidth, false);
         canvasCenter.X = this.ActualWidth / 2.0;
         canvasCenter.Y = this.ActualHeight / 2.0;

         // Code Documentation Tag 20140603_07
         xformGroup_all = new TransformGroup();
         xformGroup_all.Children.Add(
            new ScaleTransform(1, -1)
            );
         var w = this.ActualWidth;
         var h = this.ActualHeight;
         xformGroup_all.Children.Add(
            new TranslateTransform(canvasCenter.X, canvasCenter.Y));
         xformGroup_all.Children.Add(
            new ScaleTransform(
               96.0 / this.myCadViewPort.ScaleVector.x, 
               96.0 / this.myCadViewPort.ScaleVector.y, 
               canvasCenter.X, canvasCenter.Y)
            );
         xformGroup_all.Children.Add(
            new TranslateTransform(
               this.myCadViewPort.Origin.x * -96.0 / this.myCadViewPort.ScaleVector.x,
               this.myCadViewPort.Origin.y * 96.0 / this.myCadViewPort.ScaleVector.y)
            );
         xformGroup_all.Children.Add(
            new RotateTransform(
               -this.myCadViewPort.Rotation.getAsDegreesDouble())
            );
      }

      internal void updateTranformsForTranslate()
      {
         Double dx = moveDelta.X * this.myCadViewPort.ScaleVector.x / -96.0;
         Double dy = moveDelta.Y * this.myCadViewPort.ScaleVector.y / 96.0;
         this.myCadViewPort.SlideOrigin(
            dx: dx,
            dy: dy,
            dz: null);
         this.xformGroup_all.Children.Add(
               new TranslateTransform(moveDelta.X, moveDelta.Y)
            );
         this.refresh();
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
         else if (graphicItem is NVCO.Arc)
         {
            DrawGraphicItem(graphicItem as NVCO.Arc);
         }
      }

      protected void DrawGraphicItem(NVCO.Text textItem)
      {
         var scrnPt = xformGroup_all.Transform(textItem.Origin);

         var aTextBox = new TextBox();
         aTextBox.FontFamily = new FontFamily("Arial");
         Double candidateFontSize = textItem.Height * 72 /
            this.myCadViewPort.ScaleVector.y;
         if (candidateFontSize < 3) candidateFontSize = 3;
         aTextBox.FontSize = candidateFontSize;
         aTextBox.BorderBrush = Brushes.Transparent;
         aTextBox.Background = Brushes.Transparent;
         aTextBox.Margin = new Thickness(0, 0, 0, 0);
         aTextBox.BorderThickness = new Thickness(1, 1, 1, 1);
         if (aTextBox.FontSize > 10.6)
            aTextBox.Padding = new Thickness(0,0,0,0);
         else
            aTextBox.Padding = new Thickness(-6, -6, -6, -6);
         setSymbologyText(aTextBox, textItem);
         aTextBox.Text = textItem.Content;
         //aTextBox.ToolTip = textItem.GetToolTip();
         aTextBox.RenderTransformOrigin = new Point(0, 0);
         var rotAboutPt = xformGroup_all.Transform(textItem.Origin);
         var xfrmGrp = new TransformGroup();
         if (this.myCadViewPort.Rotation.getAsDegreesDouble() != 0.0)
         {
            xfrmGrp.Children.Add(
               new RotateTransform(
                  -this.myCadViewPort.Rotation.getAsDegreesDouble()
                  )
               );
         }
         if (textItem.Rotation.getAsDegreesDouble() != 0.0)
         {
            xfrmGrp.Children.Add(
               new RotateTransform(
                  -1 * textItem.Rotation.getAsDegreesDouble()
                  )
               );
         }
         aTextBox.RenderTransform = xfrmGrp;
         Canvas.SetLeft(aTextBox, scrnPt.X );/// this.myCadViewPort.ScaleVector.x);
         Canvas.SetTop(aTextBox, scrnPt.Y);/// this.myCadViewPort.ScaleVector.y);

         this.Children.Add(aTextBox);
      }

      protected void DrawGraphicItem(NVCO.Arc arcItem)
      {
         //if (null != clickSound)
         //   clickSound.PlaySync();
         SweepDirection dir = (arcItem.Deflection.getAsRadians() < 0.0) ?
            SweepDirection.Clockwise : 
            SweepDirection.Counterclockwise;

         bool largeArc = (Math.Abs(arcItem.Deflection.getAsDegreesDouble()) > 180.0) ?
            true : false;

         PathGeometry pGeom = new PathGeometry();
         PathFigure pFig = new PathFigure();
         pFig.StartPoint = new Point(arcItem.Origin.x, arcItem.Origin.y);
         pFig.Segments.Add(
            new ArcSegment(new Point(arcItem.EndPoint.x, arcItem.EndPoint.y),
               new Size(arcItem.Radius, arcItem.Radius),
               arcItem.Rotation.getAsDegreesDouble(),
               largeArc,
               dir,
               true));
         pGeom.Figures.Add(pFig);
         System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
         path.Data = pGeom;
         path.Fill = Brushes.Transparent;
         //path.Stroke = Brushes.Black;
         //path.StrokeThickness = 2.5 * itemWidthUnscale;
         //path.StrokeThickness = 2.0;
         setSymbologyNonText(path, arcItem);
         //path.ToolTip = arcItem.GetToolTip();
         path.RenderTransform = xformGroup_all;

         this.Children.Add(path);
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
         setSymbologyNonText(aLine, lineSegment);
         //aLine.ToolTip = lineSegment.GetToolTip();
         aLine.RenderTransform = xformGroup_all;

         this.Children.Add(aLine);
      }

      private void setSymbologyNonText(Shape WpfItem, Graphic graphicItem)
      {
         Feature ft = graphicItem.Feature;
         WpfItem.Stroke = ft.Color.getAsBrush();
         WpfItem.StrokeThickness = getStrokeThickness(ft);
         WpfItem.StrokeDashArray = SYMB.Style.cvrtIntToStyle(ft.Style);
         WpfItem.Opacity = 1.0 - (Double) ft.Transparency;
      }

      private void setSymbologyText(TextBox aTbx, NVCO.Text textItem)
      {
         Feature ft = textItem.Feature;
         aTbx.Foreground = ft.Color.getAsBrush();
         aTbx.Opacity = 1.0 - (Double)ft.Transparency; 
      }

      private Double getStrokeThickness(Feature itm)
      {
         const Double MAX_WT = 9.0;
         //int preWt = (itm.Weight == null) ? 0 : (int)itm.Weight;
         int preWt = (int) itm.Weight;
         Double wt = (preWt > MAX_WT) ? MAX_WT : preWt;
         wt = (wt / 2) + 0.5;
         wt /= 96.0;

         Double screenThickness = (itm.Thickness == null) ?
            0.0 : (Double)itm.Thickness;
         screenThickness /= this.myCadViewPort.ScaleVector.x;

         Double retVal = (wt < screenThickness) ? screenThickness : wt;
         retVal *= this.myCadViewPort.ScaleVector.x;
         return retVal;
      }

      private DoubleCollection getStrokeDashArray(Feature itm)
      {
         return null;
      }

      //static NVcad2dViewCanvas()
      //{
      //   DefaultStyleKeyProperty.OverrideMetadata(typeof(NVcad2dViewCanvas), 
      //        new FrameworkPropertyMetadata(typeof(NVcad2dViewCanvas)));
      //}

      public void ViewGeometryChanged()
      {
         this.establishTransforms();
         this.refresh();
      }

      public void ViewContentsChanged()
      {
         this.refresh();
      }

      internal void refresh()
      {
         this.Children.Clear();
         foreach (var grphic in this.myCadViewPort.getVisibleGraphicElements())
         {
            this.DrawGraphicItem(grphic);
         }
      }

      private void NVcad2dViewCanvas_MouseMove(object sender, MouseEventArgs e)
      {
         if (null == xformGroup_all) return;
         MousePos = xformGroup_all.Inverse.Transform(e.GetPosition(this));
         //System.Diagnostics.Debug.Print("Hi {0}  {1}", MousePos.X.ToString(),
         //   MousePos.Y.ToString());

         // PrevMousePos = MousePos;  start here Albert
      }

      public Double getWidth()
      { return this.ActualWidth; }

      public Double getHeight()
      { return this.ActualHeight; }
   }

   internal static class extensionMethods
   {
      private static Point pt;
      public static System.Windows.Point Transform
         ( this TransformGroup xfg,
            NVCFND.Point ptPreXfrm
         )
      {
         pt = new Point(ptPreXfrm.x, ptPreXfrm.y);
         return xfg.Transform(pt);
      }
   }
}
