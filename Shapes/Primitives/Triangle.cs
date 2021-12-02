
namespace Shapes.Primitives;

public class Triangle<T> : ShapeBase<T> where T : IFloatingPoint<T>
{
    public (T A, T B, T C) Edges { get; }

    protected Triangle(T a, T b, T c) : base(() => ComputeArea(a, b, c))
        => Edges = (a, b, c);

    private static Area<T> ComputeArea(T a, T b, T c)
    {
        //more precise than Heron's formula as for floats
        //https://people.eecs.berkeley.edu/~wkahan/Triangle.pdf

        // Sort a, b, c into descending order
        if (a < b)
            (a, b) = (b, a);
        if (a < c)
            (a, c) = (c, a);
        if (b < c)
            (b, c) = (c, b);

        var p = (a + (b + c)) * (c - (a - b)) * (c + (a - b)) * (a + (b - c));

        return new Area<T>(T.Create(0.25d) * T.Sqrt(p));
    }

    public static Triangle<T> CreateInstance(T a, T b, T c)
    {
        if (T.IsNaN(a) || T.Sign(a) < T.Zero)
            throw new ArgumentOutOfRangeException(nameof(a), "Side A should be a number >= 0.");
        if (T.IsNaN(b) || T.Sign(b) < T.Zero)
            throw new ArgumentOutOfRangeException(nameof(b), "Side B should be a number >= 0.");
        if (T.IsNaN(c) || T.Sign(c) < T.Zero)
            throw new ArgumentOutOfRangeException(nameof(c), "Side C should be a number >= 0.");

        if (a > (b + c) || b > (a + c) || c > (a + b))
            throw new InvalidOperationException("Triangle cannot exist. The sum of any two sides of a triangle must greater than the third side.");

        return IsRight(a, b, c)
            ? new RightTriangle<T>(a, b, c)
            : new Triangle<T>(a, b, c);
    }

    private static bool IsRight(T a, T b, T c)
    {
        var tolerance = T.Create(0.0001);

        return T.Abs(a * a - (b * b + c * c)) < tolerance
               || T.Abs(b * b - (a * a + c * c)) < tolerance
               || T.Abs(c * c - (a * a + b * b)) < tolerance;
    }

}

