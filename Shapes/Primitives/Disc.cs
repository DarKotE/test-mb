
namespace Shapes.Primitives;

public sealed class Disk<T> : ShapeBase<T> where T : IFloatingPoint<T>
{
    public T Radius { get; }

    private Disk(T radius) : base(() => new Area<T>(T.Pi * radius * radius))
        => Radius = radius;

    public static Disk<T> CreateInstance(T radius)
    {
        if (T.IsNaN(radius))
            throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be a number.");

        if (T.Sign(radius) < T.Zero)
            throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be >= 0.");

        return new Disk<T>(radius);
    }
}
