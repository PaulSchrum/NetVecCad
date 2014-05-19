using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVcad.Foundations.WorkingUnits
{
   /// <summary>
   /// Note: All conversion factors come from en.wikipedia.org on the data indicated
   /// in comments (below)
   /// </summary>
   public class Length
   {
      public static readonly Dictionary<LengthUnit, Double> ConversionFactors;

      static Length()
      {
         ConversionFactors = new Dictionary<LengthUnit, Double>();
         ConversionFactors.Add(LengthUnit.Meter, 1.0);
         ConversionFactors.Add(LengthUnit.Foot, 3048.0 / 10000.0);  // 19 May 2014
         ConversionFactors.Add(LengthUnit.USsurveyFoot, 1200.0 / 3937.0);  // 19 May 2014
         ConversionFactors.Add(LengthUnit.Inch,  3048.0 / (12.0 * 10000.0));  // 19 May 2014
         ConversionFactors.Add(LengthUnit.USsurveyInch, 1200.0 / (12.0 * 3937.0) );  // 19 May 2014
         ConversionFactors.Add(LengthUnit.Yard, 3048.0 / (3.0 * 10000.0));  // 19 May 2014
      }

      public Double Value { get; set; }
      public LengthUnit Unit { get; set; }
      public int Power { get; set; }

      public Length(Double value, LengthUnit unit) : this(value, unit, 1)
      { }

      public Length(Double value, LengthUnit unit, int power)
      {
         Value = value;
         Unit = unit;
         Power = power;
      }

      public Double GetAs(LengthUnit targetUnit)
      {
         if (this.Unit == targetUnit) return this.Value;
         // else
         var v = ConversionFactorTo(targetUnit);
         return this.Value * v;
      }

      protected Double ConversionFactorTo(LengthUnit targetUnit)
      {
         return getConversionFactorFromTo(this.Unit, targetUnit);
      }

      static Double getConversionFactorFromTo(LengthUnit from, LengthUnit to)
      {
         var frm = ConversionFactors[from];
         var too = ConversionFactors[to];
         Double v = ConversionFactors[from] / ConversionFactors[to];
         return v;
      }
   }

   public enum LengthUnit
   {
      Meter,
      Foot,
      Inch,
      USsurveyFoot,
      USsurveyInch,
      Yard
   }
}
