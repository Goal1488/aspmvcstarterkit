using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace Common
{
    public static class SimpleInjectorContainer
    {
        private static Container _kernel;

        //[Pure]
        public static Container Default => SimpleInjectorContainer._kernel ?? (SimpleInjectorContainer._kernel = new Container());

        public static void Reset()
        {
            SimpleInjectorContainer._kernel = new Container();
        }
    }
}
