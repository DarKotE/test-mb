
namespace Shapes.Primitives;

public sealed class RightTriangle<T> : Triangle<T> where T : IFloatingPoint<T>
{
    internal RightTriangle(T a, T b, T c) : base(a, b, c)
    {
    }
}
