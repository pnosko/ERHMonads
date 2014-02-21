/// This implementation was inspired by scala.util.Try from scala core library.

using System;

namespace StrangeAttractor.Util.Functional.Interfaces
{
    /// <summary>
    /// Value encapsulating a result of an operation that can fail with an exception.
    /// </summary>
    public interface ITry<out T> : IValue<T>, IOptionalValue<T>
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }

        /// <summary>
        /// Retrieve a result of applying a function on either the result of the successful operation, or to the exception thrown by that operation.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="onError">Function to be applied to an exception.</param>
        /// <param name="onSuccess">Function to be applied to an success value.</param>
        /// <returns>The result of applying one of the functions passed in.</returns>
        /// <remarks>Analogous to <see cref="System.Collections.Generic.IEnumerable{T}"/>.Aggregate.</remarks>
        TResult Fold<TResult>(Func<Exception, TResult> onError, Func<T, TResult> onSuccess);

        ITry<TResult> Select<TResult>(Func<T, TResult> selector);
        ITry<TResult> SelectMany<TResult>(Func<T, ITry<TResult>> selector);
        ITry<TResult> SelectMany<TIntermediate, TResult>(Func<T, ITry<TIntermediate>> intermediate, Func<T, TIntermediate, TResult> selector);
        ITry<T> Where(Func<T, bool> predicate);
    }
}
