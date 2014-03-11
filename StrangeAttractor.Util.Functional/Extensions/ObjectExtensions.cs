using System;
using System.Collections.Generic;
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
        /// <returns>Something, if the nullable has value, otherwise Nothing.</returns>
        public static IOption<T> ToOption<T>(this T? self) where T : struct
        {
            return !self.HasValue ? Option.Nothing<T>() : Option.Something(self.Value);
        }

        /// <summary>
        /// Lifts the value to Maybe monad.
        /// </summary>
        /// <returns>Something, if the value exists (is not null), otherwise Nothing.</returns>
        public static IOption<T> ToOption<T>(this T self)
        {
            return self.IsNull() ? Option.Nothing<T>() : Option.Something(self);
        }

        /// <summary>
        /// Casts the value to the provided type.
        /// </summary>
        /// <returns>The encapsulated value, if cast was successful, otherwise nonexistent value.</returns>
        public static IOption<T> Cast<T>(this object self)
        {
            return self.TryCast<T>().AsOption();
        }

        public static ITry<T> TryCast<T>(this object self)
        {
            return Try.Invoke(() => (T)self);
        }

        public static T DoAndReturn<T>(this T self, Action<T> action)
        {
            action(self);
            return self;
        }

        /// <summary>
        /// Wraps the element in an <see cref="IEnumerable<T>"/>.
        /// </summary>
        public static IEnumerable<T> ToEnumerable<T>(this T self)
        {
            yield return self;
        }
    }
}
