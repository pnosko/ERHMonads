using StrangeAttractor.Util.Functional.Interfaces;

namespace StrangeAttractor.Util.Functional
{
	public interface IOptionalValue<out T>
	{
		IMaybe<T> ToMaybe();
	}
}
