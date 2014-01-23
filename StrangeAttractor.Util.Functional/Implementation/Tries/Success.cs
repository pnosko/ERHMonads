using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Extensions;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation.Tries
{
	/// <summary>
	/// Successful result of an operation.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal class Success<T> : ITry<T>
	{
		public Success(T value)
		{
			this.Value = value;
		}

		public T Value { get; private set; }

		public bool IsSuccess
		{
			get { return true; }
		}

		public bool IsFailure
		{
			get { return false; }
		}

		public IMaybe<T> ToMaybe()
		{
			return this.Value.ToMaybe();
		}

		public TResult Fold<TResult>(Func<Exception, TResult> onError, Func<T, TResult> onSuccess)
		{
			return onSuccess(this.Value);
		}

		public ITry<TResult> Select<TResult>(Func<T, TResult> selector)
		{
			return Try.Invoke(() => selector(this.Value));
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
				return intermediate(this.Value).Select(x => selector(this.Value, x));
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
	}
}
