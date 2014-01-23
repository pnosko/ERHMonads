using System;

namespace StrangeAttractor.Util.Functional.Implementation.Eithers
{
	internal class Right<TLeft, TRight> : Either<TLeft, TRight>
	{
		private readonly TRight _value;

		public Right(TRight value)
		{
			this._value = value;
		}

		public override bool IsRight
		{
			get { return true; }
		}

		public override bool IsLeft
		{
			get { return false; }
		}

		public override TResult Fold<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight)
		{
			return onRight(this._value);
		}

		public TRight Get()
		{
			return this._value;
		}
	}
}
