using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Implementation.Error;

namespace StrangeAttractor.Util.Functional.Singletons
{
    public static class Try
    {
        public static ITry<T> Invoke<T>(Func<T> function)
        {
            try
            {
                return new Success<T>(function());
            }
            catch (Exception e)
            {
                return new Failure<T>(e);
            }
        }

        public static void InvokeAction(Action action)
        {
            try
            {
                action();
            }
            catch { }
        }

        internal static ITry<T> Success<T>(T value)
        {
            return new Success<T>(value);
        }

        internal static ITry<T> Failure<T>(Exception error)
        {
            return new Failure<T>(error);
        }

        /// <summary>
        /// Pretends to pattern match on Try instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="self"></param>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <returns></returns>
        /// <remarks>Does not respect contravariance.</remarks>
        internal static R Match<T, R>(this ITry<T> self, Func<Success<T>, R> success, Func<Failure<T>, R> failure)
        {
            if (self.IsSuccess)
            {
                return success((Success<T>)self);
            }
            else
            {
                return failure((Failure<T>)self);
            }
        }
    }
}
