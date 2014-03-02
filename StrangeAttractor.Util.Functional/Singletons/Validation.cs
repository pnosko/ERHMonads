using StrangeAttractor.Util.Functional.Implementation.Validations;
using StrangeAttractor.Util.Functional.Interfaces;
using System;
using System.Linq;

namespace StrangeAttractor.Util.Functional.Singletons
{
    public static class Validation
    {
        public static IValidation<TError, TValue> Failure<TError, TValue>(TError error)
        {
            return new Failure<TError, TValue>(error);
        }

        public static IValidation<Exception, TValue> Failure<TValue>(Exception error)
        {
            return new Failure<Exception, TValue>(error);
        }

        public static IValidation<TError, TValue> Success<TError, TValue>(TValue value)
        {
            return new Success<TError, TValue>(value);
        }

        public static IValidation<Exception, TValue> Success<TValue>(TValue value)
        {
            return new Success<Exception, TValue>(value);
        }
    }
}
