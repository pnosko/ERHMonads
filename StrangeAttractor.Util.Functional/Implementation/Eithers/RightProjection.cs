using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Extensions;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation.Eithers
{
	internal class RightProjection<TLeft, TRight> : IValue<TRight>, IRightProjection<TLeft, TRight>
	{
		private readonly IEither<TLeft, TRight> _either;

		public RightProjection(IEither<TLeft, TRight> either)
		{
			this._either = either;
		}

		public TRight Value
		{
			get
			{
				return this.ToMaybe().GetOrThrow(() =>
					new InvalidOperationException("No value available."));
			}
		}

		public IMaybe<TRight> ToMaybe()
		{
			return this._either.Fold(
				x => Maybe.Nothing<TRight>(),
				x => x.ToMaybe());
		}

		public IEither<TLeft, TResultRight> Select<TResultRight>(Func<TRight, TResultRight> selector)
		{
			return this._either.Fold(
				x => x as IEither<TLeft, TResultRight> ?? Either.Left<TLeft, TResultRight>(x),
				x => Either.Right<TLeft, TResultRight>(selector(x)));
		}

		public IEither<TResultLeft, TResultRight> SelectMany<TResultRight, TResultLeft>(Func<TRight, IEither<TResultLeft, TResultRight>> selector)
		{
			return this._either.Fold(x => (IEither<TResultLeft, TResultRight>)x, selector);
		}

		//public IEither<TResultLeft, TResultRight> SelectMany<TResultLeft, TIntermediate, TResultRight>(Func<TRight, IEither<TResultLeft, TIntermediate>> intermediate, Func<TRight, TIntermediate, TResultRight> selector)
		//{
		//	return this._either.Fold(
		//		x => (IEither<TResultLeft, TResultRight>)x, x => this.SelectMany(y => selector(y, intermediate(y).Right.ToMaybe().Value)));
		//}
	}
}