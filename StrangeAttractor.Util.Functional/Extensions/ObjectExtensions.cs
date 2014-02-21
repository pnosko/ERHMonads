using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull<T>(this T self)
        {
            return self == null;
        }

        /// <summary>
        /// Casts the nullable to Maybe monad.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns>Something, if the nullable has value, otherwise Nothing.</returns>
        public static IOption<T> ToOption<T>(this T? self) where T : struct
        {
            return !self.HasValue ? Option.Nothing<T>() : Option.Something(self.Value);
        }

        /// <summary>
        /// Lifts the value to Maybe monad.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns>Something, if the value exists (is not null), otherwise Nothing.</returns>
        public static IOption<T> ToOption<T>(this T self)
        {
            return self.IsNull() ? Option.Nothing<T>() : Option.Something(self);
        }

        public static T DoAndReturn<T>(this T self, Action<T> action)
        {
            action(self);
            return self;
        }
    }
}
