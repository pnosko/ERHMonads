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
        TResult Fold<TResult>(Func<T, TResult> onSome, Func<TResult> onNone);

        IOption<TResult> Select<TResult>(Func<T, TResult> selector);
        IOption<TResult> SelectMany<TResult>(Func<T, IOption<TResult>> selector);
        IOption<TResult> SelectMany<TIntermediate, TResult>(Func<T, IOption<TIntermediate>> intermediate, Func<T, TIntermediate, TResult> selector);
        IOption<T> Where(Func<T, bool> predicate);
    }
}