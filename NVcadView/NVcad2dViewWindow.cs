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
   public class NVcad2dViewWindow : Xceed.Wpf.Toolkit.ChildWindow
   {
      //static NVcad2dViewWindow()
      //{
      //   DefaultStyleKeyProperty.OverrideMetadata(typeof(NVcad2dViewWindow), new FrameworkPropertyMetadata(typeof(NVcad2dViewWindow)));
      //}
      
      public void initializeCustomSettings()
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

         var cvs = new Canvas();
         cvs.Background = Brushes.Bisque;
         contentGrid.Children.Add(cvs);
         Grid.SetRow(cvs, 1); Grid.SetColumn(cvs, 1);

         this.Content = contentGrid;
      }
   }
}
