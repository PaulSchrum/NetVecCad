using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVcad.Foundations.Symbology
{
   public class FeatureList
   {
      public SortedList<String, Feature> Children
      {
         get;
         protected set;
      }

      public Feature DefaultFeature
      {
         get;
         private set;
      }

      public FeatureList()
      {
         Children = new SortedList<String, Feature>();
         setDefaultFeature();
      }

      private void setDefaultFeature()
      {
         DefaultFeature = new Feature();
         DefaultFeature.Name = "Default";
         Children.Add(DefaultFeature.Name, DefaultFeature);
      }

      public Feature GetByName(String name)
      {
         if (this.Children.ContainsKey(name) == false) return null;
         Feature retval;
         retval = this.Children[name];
         return retval;
      }

      public void AddFeature(Feature ft)
      {
         this.Children.Add(ft.Name, ft);
      }

      public bool TryRemoveFeature(String name)
      {
         if (name == "Default") return false;
         if (this.Children.ContainsKey(name) == false) return false;
         return this.Children.Remove(name);
      }
   }
}
