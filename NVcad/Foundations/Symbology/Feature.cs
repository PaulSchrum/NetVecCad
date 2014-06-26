using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NVcad.Foundations.Symbology.Color;

namespace NVcad.Foundations.Symbology
{
   public class Feature
   {
      public String Name { get; set; }        // Default "Default"
      public NVcad.Foundations.Symbology.Color.Color Color { get; set; }  // Default White
      public int? Style { get; set; }  // Default Solid -- Always Solid for Text
      public int? Weight { get; set; }        // Default 0
      public Double? Thickness { get; set; }  // Default 0.0
      public Double? Transparency { get; set; }     // Default 0.0
      public NVcad.Foundations.Symbology.Color.Color FillColor { get; set; } // Default White
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



      public Feature()
      {
         Name = "Default";
         Color = (ColorAsBrush)System.Windows.Media.Brushes.Black;
         Style = 0;
         Weight = 0;
         Thickness = 0;
         Transparency = 0;
         FillColor = (ColorAsBrush)System.Windows.Media.Brushes.White;
         DisplayPriority = 0;
         Snapable = true;
         Printable = true;

         FontName = "Arial";
         FontSize = 100;
         Justification = new Tuple<int?, int?>(-1, -1);
         BackgroundMargin = 0.1;
         BackgroundBorderSymbology = null;
      }


      static Feature()
      {
         staticDefault = new Feature();
      }
      private static Feature staticDefaultFeature_ = null;
      protected static Feature staticDefault
      {
         get { return staticDefaultFeature_; }
         set { staticDefaultFeature_ = value; }
      }

      public static void SetStaticDefault(Feature feature)
      {
         staticDefault = feature;
      }

      public static Feature Factory_NewFeature()
      {
         if (null == staticDefault)
            return new Feature();
         else
            return staticDefault;
      }
   }
}
