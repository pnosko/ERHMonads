using System;
using System.Linq;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;
using StrangeAttractor.Util.Functional.Extensions;

namespace StrangeAttractor.Util.Functional.Implementation.Validations
{
    internal class Success<TError, TValue> : IValidation<TError, TValue>
    {
        private readonly TValue _value;

        public Success(TValue value)
        {
            this._value = value;
        }

        public bool IsSuccess { get { return true; } }
        public bool IsFailure { get { return false; } }

        public TResult Fold<TResult>(Func<TError, TResult> onLeft, Func<TValue, TResult> onRight)
        {
            return onRight(this.Value);
        }

        public IFailProjection<TError, TValue> Fail { get { return new FailProjection<TError, TValue>(this); } }

        public IValidation<TError, TResultValue> Select<TResultValue>(Func<TValue, TResultValue> selector)
        {
            return Validation.Success<TError, TResultValue>(selector(this.Value));
        }

        public TValue Value { get { return this._value; } }

        public IOption<TValue> ToOption()
        {
            return this.Value.ToOption();
        }
    }
}
