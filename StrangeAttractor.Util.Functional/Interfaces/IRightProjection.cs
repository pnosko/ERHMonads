using System;

namespace StrangeAttractor.Util.Functional.Interfaces
{
	public interface IRightProjection<out TLeft, out TRight> : IValue<TRight>, IOptionalValue<TRight>
	{
		IEither<TLeft, TResultRight> Select<TResultRight>(Func<TRight, TResultRight> selector);
		IEither<TResultLeft, TResultRight> SelectMany<TResultRight, TResultLeft>(Func<TRight, IEither<TResultLeft, TResultRight>> selector);
		IEither<TResultLeft, TResultRight> SelectMany<TResultLeft, TIntermediate, TResultRight>(Func<TRight, IRightProjection<TResultLeft, TIntermediate>> intermediate, Func<TRight, TIntermediate, TResultRight> selector);
	}
}