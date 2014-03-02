using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangeAttractor.Util.Functional.Extensions
{
    public static class ValidationExtensions
    {
        public static IValidation<TResultError, TResultValue> SelectMany<TError, TValue, TResultError, TResultValue>(this IValidation<TError, TValue> self, Func<TValue, IValidation<TResultError, TResultValue>> selector)
            where TError : TResultError
        {
            return self.Fold(
                e => Validation.Failure<TResultError, TResultValue>((TResultError)e),
                s => selector(s));
        }

        public static IValidation<TResultError, TResultValue> SelectMany<TError, TValue, TIntermediate, TResultError, TResultValue>(this IValidation<TError, TValue> self, Func<TValue, IValidation<TResultError, TIntermediate>> intermediate, Func<TValue, TIntermediate, TResultValue> selector)
            where TError : TResultError
        {
            return self.Fold(
                e => Validation.Failure<TResultError, TResultValue>((TResultError)e),
                s => intermediate(s).Select(i => selector(s, i)));
        }

        public static IValidation<TResultError, TResultValue> SelectMany<TError, TValue, TResultError, TResultValue>(this IFailProjection<TError, TValue> self, Func<TError, IValidation<TResultError, TResultValue>> selector)
            where TValue : TResultValue
        {
            return self.Fold(
                e => selector(e),
                s => Validation.Success<TResultError, TResultValue>((TResultValue)s));
        }

        //public static IValidation<TResultError, TResultValue> SelectMany<TError, TValue, TIntermediate, TResultError, TResultValue>(this IFailProjection<TError, TValue> self, Func<TError, IValidation<TIntermediate, TResultValue>> intermediate, Func<TError, TIntermediate, TResultError> selector)
        //   where TError : TResultError
        //{
        //    return null;
        //}

        //public static IValidation<TResultError, TResultValue> SelectMany<TError, TValue, TIntermediate, TResultError, TResultValue>(this IFailProjection<TError, TValue> self, Func<TError, IFailProjection<TIntermediate, TResultValue>> intermediate, Func<TError, TIntermediate, TResultError> selector)
        //   where TError : TResultError
        //{
        //    return null;
        //}
    }
}
