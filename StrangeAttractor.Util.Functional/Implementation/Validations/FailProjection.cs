using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Extensions;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation
{
	internal class FailProjection<TError, TValue> : IFailProjection<TError, TValue>
	{
		private readonly IValidation<TError, TValue> _validation;

		public FailProjection(IValidation<TError, TValue> validation)
		{
			this._validation = validation;
		}

		public TError Value
		{
			get
			{
				return this.ToMaybe().GetOrThrow(() =>
					new InvalidOperationException("No value available."));
			}
		}

		public IMaybe<TError> ToMaybe()
		{
			return this._validation.Fold(
				x => x.ToMaybe(),
				x => Maybe.Nothing<TError>());
		}

		public IValidation<TResultError, TValue> Select<TResultError>(Func<TError, TResultError> selector)
		{
			return this._validation.Fold(
				x => Validation.Failure<TResultError, TValue>(selector(x)),
				x => x as IValidation<TResultError, TValue> ?? Validation.Success<TResultError, TValue>(x));
		}

		public IValidation<TResultError, TResulTValue> SelectMany<TResultError, TResulTValue>(Func<TError, IValidation<TResultError, TResulTValue>> selector)
		{
			return this._validation.Fold(selector, x => (IValidation<TResultError, TResulTValue>)x);
		}
	}
}
