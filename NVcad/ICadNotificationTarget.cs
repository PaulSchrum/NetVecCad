using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NVcad.CadObjects;

namespace NVcad
{
   public interface ICadNotificationTarget
   {
      void ViewPortAdded(CadViewPort newViewPort);
      void DrawGraphicItem(NVcad.CadObjects.Graphic graphicItem);
   }

   public interface ICadViewChangedNotification
   {
      void ViewCreatedAnew();
      void ViewContentsChanged();
      void ViewGeometryChanged();
      //void FeatureFilterChanged();
      //void SymbologyAdapterChanged();
      //void TransparenciesChanged();

      Double getWidth();
      Double getHeight();
   }
}
