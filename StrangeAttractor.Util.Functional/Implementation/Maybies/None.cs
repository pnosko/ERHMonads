using System;
using StrangeAttractor.Util.Functional.Interfaces;

namespace StrangeAttractor.Util.Functional.Implementation.Maybies
{
	internal struct None<T> : IMaybe<T>, IEquatable<IMaybe<T>>
	{
		public static readonly IMaybe<T> Instance = new None<T>();

		public T Value { get { throw new InvalidOperationException("No value."); } }

		public bool HasValue
		{
			get { return false; }
		}

		public TResult Fold<TResult>(Func<T, TResult> onSome, Func<TResult> onNone)
		{
			return onNone();
		}

		public IMaybe<TResult> Select<TResult>(Func<T, TResult> selector)
		{
			return None<TResult>.Instance;
		}

		public IMaybe<TResult> SelectMany<TResult>(Func<T, IMaybe<TResult>> selector)
		{
			return None<TResult>.Instance;
		}

		public IMaybe<T> Where(Func<T, bool> predicate)
		{
			return this;
		}

		public bool Equals(IMaybe<T> other)
		{
			return !other.HasValue;
		}

		public IMaybe<TResult> SelectMany<TIntermediate, TResult>(Func<T, IMaybe<TIntermediate>> k, Func<T, TIntermediate, TResult> s)
		{
			return None<TResult>.Instance;
		}
	}
}