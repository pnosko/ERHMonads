/// This implementation was partially inspired by scalaz.Disjunction.Disjunction from the scalaz library
/// 
using System;

namespace StrangeAttractor.Util.Functional.Interfaces
{
    public interface IDisjunction<out TError, out TValue> : IValue<TValue>, IOptionalValue<TValue>
    {
        bool IsRight { get; }
        bool IsLeft { get; }

        TResult Fold<TResult>(Func<TError, TResult> onFailure, Func<TValue, TResult> onSuccess);

        IDisjunction<TValue, TError> Swapped();

        IDisjunction<TError, TResultValue> Select<TResultValue>(Func<TValue, TResultValue> selector);
    }
}