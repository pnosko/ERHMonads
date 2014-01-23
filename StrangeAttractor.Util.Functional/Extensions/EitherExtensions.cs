using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Implementation;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Extensions
{
	public static class EitherExtensions
	{
		/// <summary>
		/// Converts an Either to a Try based on whether the value is 'left' or 'right'.
		/// </summary>
		/// <param name="self"></param>
		/// <returns>Try instance encapsulating the 'right' value, or an exception in 'left' value.</returns>
		public static ITry<TRight> AsTry<TLeft, TRight>(this IEither<TLeft, TRight> self) where TLeft : Exception
		{
			return self.AsTry(x => x);
		}

		/// <summary>
		/// Converts an Either to a Try based on whether the value is 'left' or 'right'.
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <returns>Try instance encapsulating the 'right' value, or an exception with message taken from 'left' value.</returns>
		public static ITry<TValue> AsTry<TValue>(this IEither<string, TValue> self)
		{
			return self.AsTry(x => new Exception(x));
		}

		/// <summary>
		/// Converts an Either to a Try based on whether the value is 'left' or 'right'.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorSelector">Selector function mapping the 'left' (error) value to an exception.</param>
		/// <returns>Try instance encapsulating the 'right' value, or an exception from applying the selector function to the 'left' value.</returns>
		public static ITry<TRight> AsTry<TLeft, TError, TRight>(this IEither<TLeft, TRight> self, Func<TLeft, TError> errorSelector) where TError : Exception
		{
			return self.Fold(
					x => Try.Failure<TRight>(errorSelector(x)),
					x => Try.Success<TRight>(x));
		}

		public static IValidation<TLeft, TRight> ToValidation<TLeft, TRight>(this IEither<TLeft, TRight> self)
		{
			return self.Fold(Validation.Failure<TLeft, TRight>, Validation.Success<TLeft, TRight>);
		}
	}
}
