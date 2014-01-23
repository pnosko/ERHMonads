/// This implementation was partially inspired by scalaz.validation.Validation from the scalaz library
/// 
using System;

namespace StrangeAttractor.Util.Functional.Interfaces
{
	public interface IValidation<out TError, out TValue> : IValue<TValue>, IOptionalValue<TValue>
	{
		bool IsSuccess { get; }
		bool IsFailure { get; }

		/// <summary>
		/// Retrieve a result of applying a function on either 'left' or 'right' value.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="onLeft">Function to be applied if this is 'left'.</param>
		/// <param name="onRight">Function to be applied if this is 'right'.</param>
		/// <returns>The result of applying one of the functions passed in.</returns>
		/// <remarks>Analogous to <see cref="System.Collections.Generic.IEnumerable{T}"/>.Aggregate.</remarks>
		TResult Fold<TResult>(Func<TError, TResult> onLeft, Func<TValue, TResult> onRight);

		IFailProjection<TError, TValue> Fail { get; }

		IValidation<TError, TResultValue> Select<TResultValue>(Func<TValue, TResultValue> selector);

		IValidation<TResultError, TResultValue> SelectMany<TResultValue, TResultError>(Func<TValue, IValidation<TResultError, TResultValue>> selector);
	}
}
