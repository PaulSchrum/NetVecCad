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
      { }

      public void tempFunc()
      {
         var w = this.cadViews.Children[0] as NVcad2dViewWindow;
         var ww = w.ActualWidth;
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
