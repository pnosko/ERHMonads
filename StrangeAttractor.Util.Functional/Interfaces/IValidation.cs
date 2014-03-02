/// This implementation was partially inspired by scalaz.validation.Validation from the scalaz library
/// 
using System;

namespace StrangeAttractor.Util.Functional.Interfaces
{
    public interface IValidation<out TError, out TValue> : IValue<TValue>, IOptionalValue<TValue>
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }

        TResult Fold<TResult>(Func<TError, TResult> onFailure, Func<TValue, TResult> onSuccess);

        IFailProjection<TError, TValue> Fail { get; }

        IValidation<TError, TResultValue> Select<TResultValue>(Func<TValue, TResultValue> selector);
    }
}