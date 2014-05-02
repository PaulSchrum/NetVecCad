using NVcadModerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace NetVecCad
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      protected override void OnInitialized(EventArgs e)
      {
         base.OnInitialized(e);
      }

      protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
      {
         base.OnClosing(e);
      }

      protected override void OnMouseUp(MouseButtonEventArgs e)
      {
         base.OnMouseUp(e);
      }

      public List<Moderator> Moderators { get; set; }

      private void MainWindowContainer_Loaded(object sender, RoutedEventArgs e)
      {
         Moderators = new List<Moderator>();
         Moderators.Add(new Moderator(this, this.MainWindowContainer));
      }

      private void Window_ContentRendered(object sender, EventArgs e)
      {
         var t = new Timer(1500);
         t.Elapsed += new ElapsedEventHandler(timer_Tick);
         t.Start();
      }

      private void timer_Tick(object sender, EventArgs e)
      {
         Environment.Exit(0);
      }
   }
}
