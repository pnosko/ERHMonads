using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Extensions;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation.Eithers
{
	internal class LeftProjection<TLeft, TRight> : ILeftProjection<TLeft, TRight>
	{
		private readonly IEither<TLeft, TRight> _either;

		public LeftProjection(IEither<TLeft, TRight> either)
		{
			this._either = either;
		}

		public TLeft Value
		{
			get
			{
				return this.ToMaybe().GetOrThrow(() =>
					new InvalidOperationException("No value available."));
			}
		}

		public IMaybe<TLeft> ToMaybe()
		{
			return this._either.Fold(
				x => x.ToMaybe(),
				x => Maybe.Nothing<TLeft>());
		}

		public IEither<TResultLeft, TRight> Select<TResultLeft>(Func<TLeft, TResultLeft> selector) 
		{
			return this._either.Fold(
				x => Either.Left<TResultLeft, TRight>(selector(x)),
				x => x as IEither<TResultLeft, TRight> ?? Either.Right<TResultLeft, TRight>(x));
		}

		public IEither<TResultLeft, TResultRight> SelectMany<TResultLeft, TResultRight>(Func<TLeft, IEither<TResultLeft, TResultRight>> selector)
		{
			return this._either.Fold(selector, x => (IEither<TResultLeft, TResultRight>)x);
		}

		//public IEither<TResultLeft, TResultRight> SelectMany<TResultLeft, TIntermediate, TResultRight>(Func<TLeft, IEither<TIntermediate, TResultRight>> intermediate, Func<TLeft, TIntermediate, TResultLeft> selector)
		//{
		//	throw new NotImplementedException();
		//}
	}
}