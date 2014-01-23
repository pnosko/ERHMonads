using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Implementation;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Extensions
{
	public static class ValidationExtensions
	{
		public static IEither<TError, TValue> ToEither<TError, TValue>(this IValidation<TError, TValue> self)
		{
			return self.Fold(Either.Left<TError, TValue>, Either.Right<TError, TValue>);
		}

		public static ITry<TValue> ToTry<TError, TValue>(this IValidation<TError, TValue> self) where TError : Exception
		{
			return self.Fold(Try.Failure<TValue>, Try.Success);
		}
	}
}
