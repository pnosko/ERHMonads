/// This implementation was partially inspired by scala.util.Either the from scala core library
using System;

namespace StrangeAttractor.Util.Functional.Interfaces
{
	/// <summary>
	/// Discriminated union over types TLeft and TRight
	/// </summary>
	/// <typeparam name="TLeft">The 'left' type, typically an error.</typeparam>
	/// <typeparam name="TRight">the 'right' type, typically the value.</typeparam>
	public interface IEither<out TLeft, out TRight>
	{
		ILeftProjection<TLeft, TRight> Left { get; }
		IRightProjection<TLeft, TRight> Right { get; }

		bool IsRight { get; }
		bool IsLeft { get; }

		/// <summary>
		/// Retrieve a result of applying a function on either 'left' or 'right' value.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="onLeft">Function to be applied if this is 'left'.</param>
		/// <param name="onRight">Function to be applied if this is 'right'.</param>
		/// <returns>The result of applying one of the functions passed in.</returns>
		TResult Fold<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight);
	}
}
