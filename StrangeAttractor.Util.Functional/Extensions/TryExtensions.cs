using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Implementation;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Extensions
{
    public static class TryExtensions
    {
        /// <summary>
        /// Retrieves the encapsulated value (on success), or the provided default value (on failure).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="default">The default value in case of failure.</param>
        /// <returns></returns>
        public static T GetOrDefault<T>(this ITry<T> self, T @default)
        {
            return self.IsSuccess ? self.Value : @default;
        }

        public static ITry<T> Flatten<T>(this ITry<ITry<T>> self)
        {
            return self.SelectMany(x => x);
        }

        public static ITry<T> OrElse<T>(this ITry<T> self, Func<ITry<T>> onElse)
        {
            return self.IsSuccess ? self : onElse();
        }

        public static ITry<T> OrElse<T>(this ITry<T> self, ITry<T> onElse)
        {
            return self.IsSuccess ? self : onElse;
        }

        public static ITry<U> Recover<T, E, U>(this ITry<T> self, Func<E, U> rescue)
            where E : Exception
            where T : U
        {
            return self.Fold(e => e.Cast<E>().TrySelect(rescue).OrElse(Try.Failure<U>(e)), x => Try.Success<U>((U)x));
        }

        public static ITry<U> RecoverWith<T, E, U>(this ITry<T> self, Func<E, ITry<U>> rescue)
            where E : Exception
            where T : U
        {
            return self.Fold(
                e => e.Cast<E>().Select(rescue).GetOrElse(Try.Failure<U>(e)), 
                x => Try.Success<U>((U)x));
        }
    }
}
