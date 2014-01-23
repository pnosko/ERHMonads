using System;
using StrangeAttractor.Util.Functional.Interfaces;

namespace StrangeAttractor.Util.Functional.Implementation.Maybies
{
	internal struct Some<T> : IMaybe<T>, IEquatable<IMaybe<T>>
	{
		private readonly T _value;

		public T Value
		{
			get { return this._value; }
		}

		public bool HasValue
		{
			get { return true; }
		}

		public TResult Fold<TResult>(Func<T, TResult> onSome, Func<TResult> onNone)
		{
			return onSome(this.Value);
		}

		public IMaybe<TResult> Select<TResult>(Func<T, TResult> selector)
		{
			return new Some<TResult>(selector(this.Value));
		}

		public IMaybe<TResult> SelectMany<TResult>(Func<T, IMaybe<TResult>> selector)
		{
			return selector(this.Value);
		}

		public IMaybe<T> Where(Func<T, bool> predicate)
		{
			if (predicate(this.Value))
			{
				return this;
			}
			return new None<T>();
		}

		public Some(T value)
		{
			this._value = value;
		}

		public bool Equals(IMaybe<T> other)
		{
			return other.HasValue && this.Value.Equals(other.Value);
		}

		public IMaybe<TResult> SelectMany<TIntermediate, TResult>(Func<T, IMaybe<TIntermediate>> intermediate, Func<T, TIntermediate, TResult> selector)
		{
			var val = this.Value;
			return intermediate(val).SelectMany(x => new Some<TResult>(selector(val, x)));
		}
	}
}
