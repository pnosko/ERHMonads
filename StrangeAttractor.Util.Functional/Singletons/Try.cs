using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Implementation.Error;
using System.Threading.Tasks;

namespace StrangeAttractor.Util.Functional.Singletons
{
    public static class Try
    {
        public static ITry<T> Invoke<T>(Func<T> function)
        {
            try
            {
                return Success<T>(function());
            }
            catch (Exception e)
            {
                return Failure<T>(e);
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

        /// <summary>
        /// Creates a Try from Task.
        /// TASK MUST BE COMPLETED!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        internal static ITry<T> FromTask<T>(Task<T> task)
        {
            if(!task.IsCompleted)
            {
                return Failure<T>(new Exception("Task not completed."));
            }
            if (task.IsFaulted)
            {
                return Failure<T>(task.Exception);
            }
            return Success<T>(task.Result);
        }
    }
}
