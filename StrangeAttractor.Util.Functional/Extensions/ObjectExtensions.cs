using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangeAttractor.Util.Functional.Extensions
{
   public static class ObjectExtensions
    {
       public static bool IsNull<T>(this T self)
       {
           return self == null;
       }
    }
}
