
namespace Shapes;
public interface IShape<T> where T : IFloatingPoint<T>
{
    bool TryGetArea(out Area<T>? area);
}