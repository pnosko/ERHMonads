using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Implementation.Eithers;

namespace StrangeAttractor.Util.Functional.Singletons
{
	public static class Either
	{
		public static IEither<object, TRight> Right<TRight>(TRight rightValue)
		{
			return new Right<object, TRight>(rightValue);
		}

		public static IEither<TLeft, TRight> Right<TLeft, TRight>(TRight rightValue)
		{
			return new Right<TLeft, TRight>(rightValue);
		}

		public static IEither<TLeft, TRight> Left<TLeft, TRight>(TLeft leftValue)
		{
			return new Left<TLeft, TRight>(leftValue);
		}

		public static IEither<TLeft, object> Left<TLeft>(TLeft leftValue)
		{
			return new Left<TLeft, object>(leftValue);
		}
	}
}
