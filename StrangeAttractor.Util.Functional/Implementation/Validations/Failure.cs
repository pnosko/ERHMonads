using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Extensions;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation.Validations
{
	internal class Failure<TError, TValue> : IValidation<TError, TValue>
	{
		private readonly TError _error;

		public Failure(TError error)
		{
			_error = error;
		}

		public TValue Value { get { throw new InvalidOperationException("No value."); } }

		public bool IsSuccess { get { return false; } }

		public bool IsFailure { get { return true; } }

		public TResult Fold<TResult>(Func<TError, TResult> onLeft, Func<TValue, TResult> onRight)
		{
			return onLeft(this._error);
		}

		public IMaybe<TValue> ToMaybe()
		{
			return Maybe.Nothing<TValue>();
		}

		public IValidation<TError, TResultValue> Select<TResultValue>(Func<TValue, TResultValue> selector)
		{
			return Validation.Failure<TError, TResultValue>(this._error);
		}

		public IValidation<TResultError, TResultValue> SelectMany<TResultValue, TResultError>(Func<TValue, IValidation<TResultError, TResultValue>> selector)
		{
			return Validation.Failure<TResultError, TResultValue>(this._error.Cast<TResultError>().GetOrDefault());
		}

		public IFailProjection<TError, TValue> Fail
		{
			get { return new FailProjection<TError, TValue>(this); }
		}
	}
}
