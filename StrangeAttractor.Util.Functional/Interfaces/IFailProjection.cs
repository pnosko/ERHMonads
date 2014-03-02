/// This implementation was partially inspired by scalaz.validation.Validation.FailProjection from the scalaz library
using System;

namespace StrangeAttractor.Util.Functional.Interfaces
{
    public interface IFailProjection<out TError, out TValue> : IValue<TError>, IOptionalValue<TError>
    {
        IValidation<TResultError, TValue> Select<TResultError>(Func<TError, TResultError> selector);

        TResult Fold<TResult>(Func<TError, TResult> onFailure, Func<TValue, TResult> onSuccess);
    }
}