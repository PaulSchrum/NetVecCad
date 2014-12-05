using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NVcad.Foundations.Coordinates;
using NVcad.CadObjects;
using NVcad.Foundations;
using NVcad.Foundations.WorkingUnits;
using System.ComponentModel;
using NVcad.Foundations.Angles;
using NVcad.Foundations.Symbology;
using NVcad.Foundations.Symbology.Color;
using netDxf;
using System.IO;


namespace NVcad.Models
{
   internal class DxfHelperRead
   {
      public DxfHelperRead(String fullFileName)
      {
         if (String.IsNullOrEmpty(fullFileName)) throw new ArgumentNullException("File name is null or is empty.");
         if (!File.Exists(fullFileName)) throw new FileNotFoundException(fullFileName);

         this.dxf = DxfDocument.Load(fullFileName);
      }

      protected DxfDocument dxf { get; set; }

      public FeatureList GetFeatureList()
      {
         if (null == dxf) throw new Exception("Dxf file has not been set.");
         FeatureList fList = new FeatureList();

         foreach(var layer in dxf.Layers)
         {
            Feature feature = new Feature();
            feature.Name = layer.Name;
            feature.Color = 
               new ColorAsBrush
                  ( layer.Color.R
                  , layer.Color.G
                  , layer.Color.B
                  );
            feature.FillColor = feature.Color;
            //feature.Style = layer.LineType;
            feature.Weight = layer.Lineweight.Value;
            feature.Printable = layer.Plot;
            fList.AddFeature(feature);
         }

         return fList;
      }
   }
}
