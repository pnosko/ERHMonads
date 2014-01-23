using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Implementation.Maybies;

namespace StrangeAttractor.Util.Functional.Singletons
{
	public static class Maybe
	{
		public static IMaybe<T> Nothing<T>()
		{
			return None<T>.Instance;
		}

		public static IMaybe<T> Something<T>(T value)
		{
			return new Some<T>(value);
		}
	}
}
