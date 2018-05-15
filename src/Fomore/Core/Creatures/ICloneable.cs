namespace Core.Creatures
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}