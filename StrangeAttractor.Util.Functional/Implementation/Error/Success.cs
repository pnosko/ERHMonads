using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Extensions;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation.Error
{
    internal struct Success<T> : ITry<T>, IEquatable<ITry<T>>
    {
        private readonly T _value;

        public Success(T value)
        {
            this._value = value;
        }

        public bool IsSuccess { get { return true; } }
        public bool IsFailure { get { return false; } }
        public bool HasValue { get { return IsSuccess; } }

        public T Value { get { return this._value; } }

        public IOption<T> ToOption()
        {
            return this.Value.ToOption();
        }

        public TResult Fold<TResult>(Func<Exception, TResult> onError, Func<T, TResult> onSuccess)
        {
            return onSuccess(this.Value);
        }

        public ITry<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            var val = this.Value;
            return Try.Invoke(() => selector(val));
        }

        public ITry<TResult> SelectMany<TResult>(Func<T, ITry<TResult>> selector)
        {
            try
            {
                return selector(this.Value);
            }
            catch (Exception e)
            {
                return Try.Failure<TResult>(e);
            }
        }

        public ITry<TResult> SelectMany<TIntermediate, TResult>(Func<T, ITry<TIntermediate>> intermediate, Func<T, TIntermediate, TResult> selector)
        {
            try
            {
                var val = this.Value;
                return intermediate(this.Value).Select(x => selector(val, x));
            }
            catch (Exception e)
            {
                return Try.Failure<TResult>(e);
            }
        }

        public ITry<T> Where(Func<T, bool> predicate)
        {
            try
            {
                if (predicate(this.Value)) return this;
                return Try.Failure<T>(new InvalidOperationException("Predicate does not hold for " + this.Value));
            }
            catch (Exception e)
            {
                return Try.Failure<T>(e);
            }
        }

        public IOption<Exception> AsFailed()
        {
            return Option.Nothing<Exception>();
        }

        public bool Equals(ITry<T> other)
        {
            return other.HasValue && other.ToOption().Equals(this.ToOption());
        }
    }
}
