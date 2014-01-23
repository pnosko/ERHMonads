using System;

namespace StrangeAttractor.Util.Functional.Interfaces
{
	/// <summary>
	/// The left projection of <see cref="IEither{TLeft, TRight}"/> used for manipulating the 'left' value.
	/// </summary>
	public interface ILeftProjection<out TLeft, out TRight> : IValue<TLeft>, IOptionalValue<TLeft>
	{
		IEither<TResultLeft, TRight> Select<TResultLeft>(Func<TLeft, TResultLeft> selector);
		IEither<TResultLeft, TResultRight> SelectMany<TResultLeft, TResultRight>(Func<TLeft, IEither<TResultLeft, TResultRight>> selector);
		//IEither<TResultLeft, TResultRight> SelectMany<TResultLeft, TIntermediate, TResultRight>(Func<TLeft, IEither<TIntermediate, TResultRight>> intermediate, Func<TLeft, TIntermediate, TResultLeft> selector);
	}
}