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
using System.Timers;
using System.Windows.Threading;

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

      System.Timers.Timer t = new Timer(16);
      public void CreateNewEmptyModel()
      {
         if (null == cadViews) return;
         // todo: offer to save the existing open model if necessary
         cadViews.Children.Clear();
         this.Model = new Model(this);  // Code Documentation Tag 20140603_01
         Model.setUpTestingModel_20140422();


         t.Elapsed += new ElapsedEventHandler((sender_, e_) => { finishBuildingChildWindow(this); });
         t.Start();
      }

      protected void finishBuildingChildWindow(Moderator aMod)
      {
         NVcad2dViewWindow wndow = null;
         double wndowwidth = 0.0;
         this.myParentWindow.Dispatcher.BeginInvoke(new Action(() =>
         {
            wndow = aMod.cadViews.Children[0] as NVcad2dViewWindow;
            aMod.t.Stop();
            aMod.t.Interval = 5;
            wndowwidth = wndow.ActualWidth;
            //System.Diagnostics.Debug.Print("Canvas Width = {0}", ww);
            if (wndowwidth > 0.0)
            {
               aMod.t.Enabled = false;
               // Code Documentation Tag 20140603_06
               aMod.establishAllViewTransforms();
            }
         }));
         if (wndowwidth == 0.0) return;
      }

      private void establishAllViewTransforms()
      {  // Code Documentation Tag 20140603_06
         foreach (NVcad2dViewWindow view in this.cadViews.Children)
         {
            view.establishTransforms();
            view.refresh();  // Code Documentation Tag 20140603_08
         }
      }

      private void refreshAllViews()
      {
         foreach (NVcad2dViewWindow view in this.cadViews.Children)
         {
            view.refresh();
         }
      }

      public void ViewPortAdded(CadViewPort newViewPort)
      {  // Code Documentation Tag 20140603_03
         if (null == cadViews) return;  // todo: throw exception
         cadViews.AddChildWindow(newViewPort);
      }

      public void DrawGraphicItem(Graphic graphicItem)
      { }

   }

   public static class ModeratorExtensionMethods
   {
      public static NVcad2dViewWindow AddChildWindow
         (this WindowContainer container,
         CadViewPort newViewPort)
      {  // Code Documentation Tag 20140603_04
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
