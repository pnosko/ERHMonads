using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Implementation.Maybe;

namespace StrangeAttractor.Util.Functional.Singletons
{
    public static class Option
    {
        public static IOption<T> Nothing<T>()
        {
            return None<T>.Instance;
        }

        public static IOption<T> Something<T>(T value)
        {
            return new Some<T>(value);
        }
    }
}
