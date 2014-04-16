using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVcad.Foundations.Symbology
{
   public class Feature
   {
      public String Name { get; set; }       // Default "Default"
      NVcad.Foundations.Symbology.Color Color { get; set; }  // Default White
      int? Style { get; set; }               // Default Solid -- Always Solid for Text
      int? Weight { get; set; }              // Default 0
      Double? Thickness { get; set; }        // Default 0.0
      Double? Transparency { get; set; }     // Default 0.0
      int? DisplayPriority { get; set; }     // Default 0
      bool? Snapable { get; set; }           // Default true
      bool? Printable { get; set; }          // Defalt true

      // Only applicable to Text Items
      String FontName { get; set; }          // Default Arial
      Double? FontSize { get; set; }         // Default 100 (equiv to 12 point on 1" = 50' scale)
      Tuple<int?, int?> Justification { get; set; } // Default {-1, -1}, Left Top
      NVcad.Foundations.Symbology.Color BackgroundColor { get; set; } // Default Black
      Double? BackgroundTransparency { get; set; } // Default 1.0 = 100%
      Double? BackgroundMargin { get; set; } // Default 0.1 == 10% of FontSize
   }
}
