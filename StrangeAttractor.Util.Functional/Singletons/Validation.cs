using StrangeAttractor.Util.Functional.Implementation.Disjunctions;
using StrangeAttractor.Util.Functional.Interfaces;
using System;
using System.Linq;

namespace StrangeAttractor.Util.Functional.Singletons
{
    public static class Disjunction
    {
        public static IDisjunction<TError, TValue> Failure<TError, TValue>(TError error)
        {
            return new Failure<TError, TValue>(error);
        }

        public static IDisjunction<Exception, TValue> Failure<TValue>(Exception error)
        {
            return new Failure<Exception, TValue>(error);
        }

        public static IDisjunction<TError, TValue> Success<TError, TValue>(TValue value)
        {
            return new Success<TError, TValue>(value);
        }

        public static IDisjunction<Exception, TValue> Success<TValue>(TValue value)
        {
            return new Success<Exception, TValue>(value);
        }
    }
}
