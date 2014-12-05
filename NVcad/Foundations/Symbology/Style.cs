using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NVcad.Foundations.Symbology
{
   public class Style
   {
      public DoubleCollection StyleDoubleValues { get; protected set; }

      private static Style cvrt_IntToStyle(int i)
      {
         Style aStyle = new Style();
         switch (i)
         {
            case 0:
               break;
            case 1:
               aStyle.StyleDoubleValues = new DoubleCollection() { 12, 4 };
               break;
            case 2:
               aStyle.StyleDoubleValues = new DoubleCollection() { 7, 4 };
               break;
            case 3:
               aStyle.StyleDoubleValues = new DoubleCollection() { 2, 2 };
               break;
            default:
               break;
         }
         return aStyle;
      }
      
      public static DoubleCollection cvrtIntToStyle(int i)
      {
         return (cvrt_IntToStyle(i)).StyleDoubleValues;
      }

      public static DoubleCollection cvrtIntToStyle(int? iNl)
      {
         int i = (int) iNl;
         return (cvrt_IntToStyle(i)).StyleDoubleValues;
      }

   }
}
