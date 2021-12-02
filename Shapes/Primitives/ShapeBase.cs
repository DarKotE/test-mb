
using System.Diagnostics.CodeAnalysis;

namespace Shapes.Primitives;

public abstract class ShapeBase<T> : IShape<T> where T : IFloatingPoint<T>
{
    protected Lazy<Area<T>> Area { get; }

    protected ShapeBase(Func<Area<T>> areaCompute) => Area = new(areaCompute);

    public bool TryGetArea([NotNullWhen(true)] out Area<T>? area)
    {
        if (T.IsInfinity(Area.Value.Value) || T.IsNaN(Area.Value.Value))
        {
            area = null;
            return false;
        }

        area = Area.Value;
        return true;
    }
}

