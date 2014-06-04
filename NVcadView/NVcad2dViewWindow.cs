using NVcad;
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Primitives;

using NVcad.CadObjects;
using System.Media;


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
   ///     "Add Reference"->"Projects"->[Select this project]
   ///
   ///
   /// Step 2)
   /// Go ahead and use your control in the XAML file.
   ///
   ///     <MyNamespace:CustomControl1/>
   ///
   /// </summary>
   public class NVcad2dViewWindow : Xceed.Wpf.Toolkit.ChildWindow, IcadView
   {
      //static NVcad2dViewWindow()
      //{
      //   DefaultStyleKeyProperty.OverrideMetadata(typeof(NVcad2dViewWindow), new FrameworkPropertyMetadata(typeof(NVcad2dViewWindow)));
      //}

      public NVcad2dViewWindow(CadViewPort newViewPort)
      {
         initializeCustomSettings(newViewPort);
         Caption = "View " + newViewPort.Name;
      }

      public NVcad2dViewCanvas primaryCanvas { get; private set; }

      public void initializeCustomSettings(CadViewPort newViewPort)
      {
         this.Height = 500;
         this.Width = 550;
         this.Left = 10;
         this.Top = 10;
         this.Caption = "NVCad 视图窗口";
         this.IsModal = false;

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

         // Code Documentation Tag 20140603_05
         primaryCanvas = new NVcad2dViewCanvas(newViewPort);
         primaryCanvas.Background = Brushes.Bisque;
         //primaryCanvas.establishTransforms();
         contentGrid.Children.Add(primaryCanvas);
         Grid.SetRow(primaryCanvas, 1); Grid.SetColumn(primaryCanvas, 1);

         this.Content = contentGrid;
      }

      public void establishTransforms()
      {  // Code Documentation Tag 20140603_06
         primaryCanvas.establishTransforms();
      }

      public void associateCadViewPort(CadViewPort aCVP)
      {
         this.primaryCanvas.myCadViewPort = aCVP;
      }

      public void updateYourself()
      {
         
      }

      public void refresh()
      {
         this.primaryCanvas.refresh();
      }
   }
}
