using System;

namespace StrangeAttractor.Util.Functional.Implementation.Eithers
{
	internal class Left<TLeft, TRight> : Either<TLeft, TRight>
	{
		private readonly TLeft _value;

		public Left(TLeft value)
		{
			this._value = value;
		}

		public override bool IsRight
		{
			get { return false; }
		}

		public override bool IsLeft
		{
			get { return true; }
		}

		public override TResult Fold<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight)
		{
			return onLeft(this._value);
		}

		public TLeft Get()
		{
			return this._value;
		}
	}
}