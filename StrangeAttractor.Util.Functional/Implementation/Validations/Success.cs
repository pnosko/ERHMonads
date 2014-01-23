using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Extensions;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation.Validations
{
	internal class Success<TError, TValue> : IValidation<TError, TValue>
	{
		private readonly TValue _value;

		public Success(TValue value)
		{
			_value = value;
		}

		public bool IsSuccess { get { return true; } }

		public bool IsFailure { get { return false; } }

		public TValue Value { get { return this._value; } }

		public TResult Fold<TResult>(Func<TError, TResult> onLeft, Func<TValue, TResult> onRight)
		{
			return onRight(_value);
		}

		public IMaybe<TValue> ToMaybe()
		{
			return this._value.ToMaybe();
		}

		public IValidation<TError, TResultValue> Select<TResultValue>(Func<TValue, TResultValue> selector)
		{
			return Validation.Success<TError, TResultValue>(selector(this._value));
		}

		public IValidation<TResultError, TResultValue> SelectMany<TResultValue, TResultError>(Func<TValue, IValidation<TResultError, TResultValue>> selector)
		{
			return selector(this._value);
		}

		public IFailProjection<TError, TValue> Fail
		{
			get { return new FailProjection<TError, TValue>(this); }
		}
	}
}
