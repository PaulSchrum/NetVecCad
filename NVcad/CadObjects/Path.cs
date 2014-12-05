using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVcad.Foundations.Coordinates;

namespace NVcad.CadObjects
{
   public abstract class Path : Geometric
   {
      public Path() : base() { }

      public Point EndPoint { get; set; }

      public Double Length { 
         get
         {
            return (this.EndPoint - this.Origin).Length;
         }
         }
   }
}
