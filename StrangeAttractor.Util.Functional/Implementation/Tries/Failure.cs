using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation.Tries
{
	internal class Failure<T> : ITry<T>
	{
		private readonly Exception _exception;

		public Failure(Exception exception)
		{
			this._exception = exception;
		}

		public T Value { get { throw this.Error; } }

		public bool IsSuccess { get { return false; } }
		public bool IsFailure { get { return true; } }

		public Exception Error
		{
			get { return this._exception; }
		}

		public IMaybe<T> ToMaybe()
		{
			return Maybe.Nothing<T>();
		}

		public TResult Fold<TResult>(Func<Exception, TResult> onError, Func<T, TResult> onSuccess)
		{
			return onError(this.Error);
		}

		public ITry<TResult> Select<TResult>(Func<T, TResult> selector)
		{
			return this as ITry<TResult>;
		}

		public ITry<TResult> SelectMany<TResult>(Func<T, ITry<TResult>> selector)
		{
			return this as ITry<TResult>;
		}

		public ITry<TResult> SelectMany<TIntermediate, TResult>(Func<T, ITry<TIntermediate>> intermediate, Func<T, TIntermediate, TResult> selector)
		{
			return this as ITry<TResult>;
		}

		public ITry<T> Where(Func<T, bool> predicate)
		{
			return this;
		}
	}
}
