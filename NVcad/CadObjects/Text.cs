using NVcad.Foundations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Coordinates;

namespace NVcad.CadObjects
{
   public class Text : Graphic
   {
      public String Content { get; set; }
      public Double Height { get; set; }
      //public Double Stretch { get; set; }
      public Angle rotationAngle { get; set; }
      public TextJustification justification { get; set; }

      protected Text() { }

      public Text(String someContent, Point aPoint) : this()
      {
         base.Origin = aPoint;
         this.Content = someContent;
      }

   }

   public enum TextJustification
   {
      LT = 1,
      CT = 2,
      RT = 3,
      LC = 4,
      CC = 5,
      RC = 6,
      LB = 7,
      CB = 8,
      RB = 9
   }
}
