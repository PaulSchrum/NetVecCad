using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTestNVcad;

namespace Console_DoStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            var methodInfos = 
                typeof(TestClassForCadFoundations)
                .GetMethods()
                .Where(method => 
                   (method.IsPublic == true)
                   && (method.IsVirtual == false)
                   && !(method.Name.Contains("GetType")))
                .OrderBy(method => method.Name)
                .ToArray()
                ;

            Console.WriteLine("TestClassForCadFoundations has {0} methods.", methodInfos.Count());
            foreach( var mi in methodInfos)
            {
                Console.WriteLine(mi.ToString());
            }
            Console.WriteLine("TestClassForCadFoundations has {0} methods.", methodInfos.Count());
            Console.ReadKey();
        }
    }
}
