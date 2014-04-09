using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVcad.Foundations.Coordinates
{
   public class StationOffsetElevation
   {
      public StationOffsetElevation() { offset = 0.0; elevation = 0.0; }
      public StationOffsetElevation(Double aStation, Double anOffset, Double anElevation)
      {
         station = aStation; offset = anOffset; elevation = anElevation;
      }

      public StationOffsetElevation(StationOffsetElevation soeOther)
      {
         station = soeOther.station;
         offset = soeOther.offset;
         elevation = soeOther.elevation;
      }

      public Double station { get; set; }
      public Double offset { get; set; }
      public Double elevation { get; set; }

      public override string ToString()
      {
         return station.ToString() + " " + offset.ToString() + "  (EL: " + elevation.ToString() + ")";
      }
   }
}
