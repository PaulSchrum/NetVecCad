using NVcadModerator;
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

      protected override void OnMouseUp(MouseButtonEventArgs e)
      {
         base.OnMouseUp(e);
      }

      public List<Moderator> Moderators { get; set; }

      private void DevCanvas_Loaded(object sender, RoutedEventArgs e)
      {
         Moderators = new List<Moderator>();
         Moderators.Add(new Moderator(this.DevCanvas));
      }
      

   }
}
