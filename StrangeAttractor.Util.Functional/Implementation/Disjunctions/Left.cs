using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation.Disjunctions
{
    internal struct Left<TError, TValue> : IDisjunction<TError, TValue>, IEquatable<IDisjunction<TError, TValue>>
    {
        private readonly TError _exception;

        public Left(TError exception)
        {
            this._exception = exception;
        }

        public TValue Value { get { throw new InvalidOperationException("No value."); } }

        public bool IsRight { get { return false; } }
        public bool IsLeft { get { return true; } }
        public bool HasValue { get { return IsRight; } }

        internal TError Error
        {
            get { return this._exception; }
        }

        public IOption<TValue> AsOption()
        {
            return Option.Nothing<TValue>();
        }

        public TResult Fold<TResult>(Func<TError, TResult> onError, Func<TValue, TResult> onSuccess)
        {
            return onError(this.Error);
        }

        public IDisjunction<TError, TResult> Select<TResult>(Func<TValue, TResult> selector)
        {
            return GetFailure<TResult>();
        }

        public IDisjunction<TError, TValue> Where(Func<TValue, bool> predicate)
        {
            return this;
        }

        private IDisjunction<TError, TResult> GetFailure<TResult>()
        {
            return Disjunction.Left<TError, TResult>(this.Error);
        }

        public bool Equals(IDisjunction<TError, TValue> other)
        {
            return !other.HasValue;
        }

        public IDisjunction<TValue, TError> Swapped()
        {
            return Disjunction.Right<TValue, TError>(this.Error);
        }
    }
}
