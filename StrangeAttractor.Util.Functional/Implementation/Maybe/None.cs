using System;
using StrangeAttractor.Util.Functional.Interfaces;

namespace StrangeAttractor.Util.Functional.Implementation.Maybe
{
    internal struct None<T> : IOption<T>, IEquatable<IOption<T>>
    {
        public static readonly IOption<T> Instance = new None<T>();

        public T Value { get { throw new InvalidOperationException("No value."); } }

        public bool HasValue
        {
            get { return false; }
        }

        public TResult Fold<TResult>(Func<T, TResult> onSome, Func<TResult> onNone)
        {
            return onNone();
        }

        public IOption<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return None<TResult>.Instance;
        }

        public IOption<TResult> SelectMany<TResult>(Func<T, IOption<TResult>> selector)
        {
            return None<TResult>.Instance;
        }

        public IOption<T> Where(Func<T, bool> predicate)
        {
            return this;
        }

        public bool Equals(IOption<T> other)
        {
            return !other.HasValue;
        }

        public IOption<TResult> SelectMany<TIntermediate, TResult>(Func<T, IOption<TIntermediate>> k, Func<T, TIntermediate, TResult> s)
        {
            return None<TResult>.Instance;
        }
    }
}