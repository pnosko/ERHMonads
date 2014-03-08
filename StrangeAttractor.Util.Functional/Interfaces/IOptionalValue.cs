using StrangeAttractor.Util.Functional.Interfaces;

namespace StrangeAttractor.Util.Functional
{
    public interface IOptionalValue<out T>
    {
        IOption<T> AsOption();
    }
}
