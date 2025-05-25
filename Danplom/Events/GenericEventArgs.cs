namespace Danplom.Events;

public class GenericEventArgs<T> : EventArgs
{
    public T Value { get; }

    public GenericEventArgs(T value)
         => Value = value;
}
