namespace StrangeAttractor.Util.Functional.Interfaces
{
    /// <summary>
    /// Encapsulates a value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValue<out T>
    {
        T Value { get; }
        bool HasValue { get; }
    }
}