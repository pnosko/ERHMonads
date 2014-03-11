using System;
using System.Collections.Generic;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Extensions
{
    public static class ValidationExtensions
    {
        public static TResultValue GetOrElse<TError, TValue, TResultValue>(this IDisjunction<TError, TValue> self, Func<TResultValue> @default)
            where TValue : TResultValue
            where TResultValue : class  // TODO: Remove this limitation
        {
            return self.AsOption().As<TResultValue>().GetOrElse(@default);
        }

        public static IValidation<TResultError, TResultValue> OrElse<TError, TValue, TResultError, TResultValue>(this IValidation<TError, TValue> self, Func<IValidation<TResultError, TResultValue>> @default)
            where TValue : TResultValue
            where TError : TResultError
        {
            // TODO: Test
            return self.Fold(
                e => @default(),
                s => (IValidation<TResultError, TResultValue>)self);
        }

        public static IValidation<IEnumerable<TError>, TValue> AsAggregate<TError, TValue>(this IValidation<TError, TValue> self)
        {
            return self.Fold(
                e => Validation.Failure<IEnumerable<TError>, TValue>(e.ToEnumerable()),
                s => Validation.Success<IEnumerable<TError>, TValue>(s));
        }

        public static IValidation<TResultError, TResultValue> SelectManyApplicative<TError, TValue, TResultError, TErrorElem, TResultValue>(this IValidation<TError, TValue> self, Func<TValue, IValidation<TErrorElem, TResultValue>> selector)
            where TValue : TResultValue
            where TResultError : IEnumerable<TErrorElem>
            where TError : TResultError
        {
            return null;
        }
    }
}
