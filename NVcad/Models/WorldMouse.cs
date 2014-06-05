using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using NVcad.Foundations.Coordinates;

namespace NVcad.Models
{
   public class WorldMouse : INotifyPropertyChanged
   {
      public WorldMouse()
      {
      }

      private Double pointX_;
      public Double PointX
      {
         get { return pointX_; }
         set
         {
            pointX_ = value;
            RaisePropertyChanged("PointX");
         }
      }

      private Double pointY_;
      public Double PointY
      {
         get { return pointY_; }
         set
         {
            pointY_ = value;
            RaisePropertyChanged("PointY");
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
      public void RaisePropertyChanged(String prop)
      {
         if (null != PropertyChanged)
         {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
         }
      }
   }
}
