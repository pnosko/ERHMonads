using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangeAttractor.Util.Functional.Extensions
{
   public static class CollectionExtensions
    {
       public static bool IsEmpty<T>(this ICollection<T> self)
       {
           return self.Count == 0;
       }

       public static HashSet<T> ToHashSet<T>(this IEnumerable<T> self)
       {
           return new HashSet<T>(self);
       }

       public static IEnumerable<T> Cons<T>(this T head, IEnumerable<T> tail)
       {
           yield return head;
           foreach (var item in tail)
           {
               yield return item;

           }
       }
    }
}
