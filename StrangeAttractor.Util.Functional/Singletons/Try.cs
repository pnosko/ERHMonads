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
    }
}
