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

      public List<Moderator> Moderators { get; set; }
      public Moderator activeModerator;

      private void MainWindowContainer_Loaded(object sender, RoutedEventArgs e)
      {
         Moderators = new List<Moderator>();
         activeModerator = new Moderator(this, this.MainWindowContainer);
         Moderators.Add(activeModerator);
      }

      private void Window_ContentRendered(object sender, EventArgs e)
      {
         if (null != activeModerator)
         {
            activeModerator.CreateNewEmptyModel();
         }
         var t = new Timer(1400);
         t.Elapsed += new ElapsedEventHandler((sender_, e_) => { Environment.Exit(0); });
         t.Start();
      }

   }
}
