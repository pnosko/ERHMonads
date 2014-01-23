using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Implementation.Validations;

namespace StrangeAttractor.Util.Functional.Singletons
{
    public static class Validation
    {
        public static IValidation<TError, TValue> Failure<TError, TValue>(TError error)
        {
            return new Failure<TError, TValue>(error);
        }

        public static IValidation<TError, TValue> Success<TError, TValue>(TValue value)
        {
            return new Success<TError, TValue>(value);
        }
    }
}
