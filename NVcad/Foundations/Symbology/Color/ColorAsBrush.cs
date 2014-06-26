using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NVcad.Foundations.Symbology.Color
{
   public class ColorAsBrush : Color
   {
      public SolidColorBrush value
      {
         get;
         set;
      }

      public ColorAsBrush() : base()
      {
         value = null;
      }

      public ColorAsBrush(SolidColorBrush newBrush)
         : this()
      {
         value = newBrush;
      }

      public override SolidColorBrush getAsBrush()
      {
         return value;
      }

      public static explicit operator ColorAsBrush(SolidColorBrush aBrush)
      {
         return new ColorAsBrush(aBrush);
      }
   }
}
