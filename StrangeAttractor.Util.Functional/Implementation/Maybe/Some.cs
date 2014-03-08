using System;
using StrangeAttractor.Util.Functional.Interfaces;

namespace StrangeAttractor.Util.Functional.Implementation.Maybe
{
    internal struct Some<T> : IOption<T>, IEquatable<IOption<T>>
    {
        private readonly T _value;

        public Some(T value)
        {
            this._value = value;
        }

        public T Value
        {
            get { return this._value; }
        }

        public bool HasValue
        {
            get { return true; }
        }

        public TResult Fold<TResult>(Func<T, TResult> onSome, Func<TResult> onNone)
        {
            return onSome(this.Value);
        }

        public IOption<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return new Some<TResult>(selector(this.Value));
        }

        public IOption<TResult> SelectMany<TResult>(Func<T, IOption<TResult>> selector)
        {
            return selector(this.Value);
        }

        public IOption<T> Where(Func<T, bool> predicate)
        {
            if (predicate(this.Value))
            {
                return this;
            }
            return new None<T>();
        }

        public bool Equals(IOption<T> other)
        {
            return other.HasValue && this.Value.Equals(other.Value);
        }

        public IOption<TResult> SelectMany<TIntermediate, TResult>(Func<T, IOption<TIntermediate>> intermediate, Func<T, TIntermediate, TResult> selector)
        {
            var val = this.Value;
            return intermediate(val).SelectMany(x => new Some<TResult>(selector(val, x)));
        }

        public IOption<TResult> As<TResult>() where TResult : class
        {
            return from m in this
                   let t = m as TResult
                   where t != null
                   select t;
        }
    }
}
