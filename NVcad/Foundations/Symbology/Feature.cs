using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVcad.Foundations.Symbology
{
   public class Feature
   {
      public String Name { get; set; }        // Default "Default"
      public NVcad.Foundations.Symbology.Color Color { get; set; }  // Default White
      public int? Style { get; set; }  // Default Solid -- Always Solid for Text
      public int? Weight { get; set; }        // Default 0
      public Double? Thickness { get; set; }  // Default 0.0
      public Double? Transparency { get; set; }     // Default 0.0
      public NVcad.Foundations.Symbology.Color FillColor { get; set; } // Default White
      public Double? FillTransparency { get; set; } // Default 0.0
      public int? DisplayPriority { get; set; }     // Default 0
      public bool? Snapable { get; set; }           // Default true
      public bool? Printable { get; set; }          // Defalt true

      // Only applicable to Text Items
      public String FontName { get; set; }          // Default Arial
      public Double? FontSize { get; set; }         // Default 100 (equiv to 12 point on 1" = 50' scale)
      public Tuple<int?, int?> Justification { get; set; } // Default {-1, -1}, Left Top
      public Double? BackgroundMargin { get; set; } // Default 0.1 == 10% of FontSize
      public Feature BackgroundBorderSymbology { get; set; } // Default null.
   }
}
