/// This implementation was partially inspired by scala.Option from scala core library, and partially by the article http://lanshiva.blogspot.co.at/2011/02/applied-functional-programming-in-c.html.

using System;

namespace StrangeAttractor.Util.Functional.Interfaces
{
    /// <summary>
    /// Encapsulates a value, or nothing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <summary>Maybe Monad.</summary>
    public interface IOption<out T> : IValue<T>
    {
        bool HasValue { get; }

        /// <summary>
        /// Retrieve a result of applying a function on either the existent value, or to nothing.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="onSome">Function to be applied to an existing value.</param>
        /// <param name="onNone">Function to generate a result in the absence of a value.</param>
        /// <returns>The result of applying one of the functions passed in.</returns>
        /// <remarks>Analogous to <see cref="System.Collections.Generic.IEnumerable{T}"/>.Aggregate.</remarks>
        TResult Fold<TResult>(Func<T, TResult> onSome, Func<TResult> onNone);

        IOption<TResult> Select<TResult>(Func<T, TResult> selector);
        IOption<TResult> SelectMany<TResult>(Func<T, IOption<TResult>> selector);
        IOption<TResult> SelectMany<TIntermediate, TResult>(Func<T, IOption<TIntermediate>> k, Func<T, TIntermediate, TResult> s);
        IOption<T> Where(Func<T, bool> predicate);
    }
}