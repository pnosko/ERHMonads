using StrangeAttractor.Util.Functional.Implementation.Disjunctions;
using StrangeAttractor.Util.Functional.Interfaces;
using System;
using System.Linq;

namespace StrangeAttractor.Util.Functional.Singletons
{
    public static class Disjunction
    {
        public static IDisjunction<TError, TValue> Left<TError, TValue>(TError error)
        {
            return new Left<TError, TValue>(error);
        }

        public static IDisjunction<Exception, TValue> Left<TValue>(Exception error)
        {
            return new Left<Exception, TValue>(error);
        }

        public static IDisjunction<TError, TValue> Right<TError, TValue>(TValue value)
        {
            return new Right<TError, TValue>(value);
        }

        public static IDisjunction<Exception, TValue> Right<TValue>(TValue value)
        {
            return new Right<Exception, TValue>(value);
        }
    }
}
