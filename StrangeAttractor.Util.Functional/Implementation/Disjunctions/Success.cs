using System;
using System.Linq;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;
using StrangeAttractor.Util.Functional.Extensions;

namespace StrangeAttractor.Util.Functional.Implementation.Disjunctions
{
    internal struct Success<TError, TValue> : IDisjunction<TError, TValue>, IEquatable<IDisjunction<TError, TValue>>
    {
        private readonly TValue _value;

        public Success(TValue value)
        {
            this._value = value;
        }

        public bool IsRight { get { return true; } }
        public bool IsLeft { get { return false; } }
        public bool HasValue { get { return IsRight; } }

        public TResult Fold<TResult>(Func<TError, TResult> onLeft, Func<TValue, TResult> onRight)
        {
            return onRight(this.Value);
        }

        public IDisjunction<TError, TResultValue> Select<TResultValue>(Func<TValue, TResultValue> selector)
        {
            return Disjunction.Success<TError, TResultValue>(selector(this.Value));
        }

        public TValue Value { get { return this._value; } }

        public IOption<TValue> AsOption()
        {
            return this.Value.ToOption();
        }

        public bool Equals(IDisjunction<TError, TValue> other)
        {
            return other.HasValue && other.AsOption().Equals(this.AsOption());
        }

        public IDisjunction<TValue, TError> Swapped()
        {
            return Disjunction.Failure<TValue, TError>(this.Value);
        }
    }
}
