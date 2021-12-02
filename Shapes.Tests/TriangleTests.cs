using NUnit.Framework;
using Shapes.Primitives;

namespace Shapes.Tests
{
    [TestFixture]
    public class TriangleTests
    {
        public static double[] IllegalValues = { -1.0, -float.Epsilon, double.MinValue, double.NaN, double.NegativeInfinity};

        [Test]
        [Pairwise]
        public void CreateInstance_Should_Throw(
            [ValueSource(nameof(IllegalValues))] double a,
            [ValueSource(nameof(IllegalValues))] double b,
            [ValueSource(nameof(IllegalValues))] double c)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Triangle<float>.CreateInstance((float)a, (float)b, (float)c));
            Assert.Throws<ArgumentOutOfRangeException>(() => Triangle<double>.CreateInstance(a, b, c));
        }

        [TestCase(0, 0, 0)]
        [TestCase(1.0, 1.0, 2.0)]
        [TestCase(float.Epsilon, float.Epsilon, float.Epsilon)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        public void CreateInstance_Should_Not_Throw(
            double a, double b,double c)
        {
            Assert.DoesNotThrow(() => Triangle<float>.CreateInstance((float)a, (float)b, (float)c));
            Assert.DoesNotThrow(() => Triangle<double>.CreateInstance(a, b, c));
        }

        [TestCase(5, 4, 10)]
        [TestCase(10, 5, 4)]
        [TestCase(5, 10, 4)]
        public void CreateInstance_Should_Throw_ImpossibleTriangle(
            double a, double b, double c)
        {
            Assert.Throws<InvalidOperationException>(() => Triangle<float>.CreateInstance((float)a, (float)b, (float)c));
            Assert.Throws<InvalidOperationException>(() => Triangle<double>.CreateInstance(a, b, c));
        }

        [TestCase(12d, 5d, 13d)]
        [TestCase(1.07f, 2.14f, 2.39259f)]
        [TestCase(1.07d, 2.14d, 2.39259d)]
        public void CreateInstance_Should_Success_RightTriangle<T>(
            T a, T b, T c) where T : IFloatingPoint<T>
        {
            Assert.True(Triangle<T>.CreateInstance(a, b, c) is RightTriangle<T>);
        }

        [TestCase(0f, 0f, 0f, 0f)]
        [TestCase(2, 3, 4, 2.9047375096555626638844490498367997081246912789687)]
        [TestCase(22.123f, 13.345f, 31.987f, 117.83411143294751466345958831366515236021857367383f)]
        public void TryGetArea_Should_Compute<T>(T a, T b, T c, T expectedAreaF) where T : IFloatingPoint<T>
        {
            //Arrange
            var diskToTest = Triangle<T>.CreateInstance(a,b,c);
            var expectedArea = new Area<T>(expectedAreaF);

            //Act
            var isComputable = diskToTest.TryGetArea(out var actualArea);

            //Assert
            Assert.True(isComputable);
            Assert.That(actualArea, Is.EqualTo(expectedArea));
            Assert.That(a, Is.EqualTo(diskToTest.Edges.A));
            Assert.That(b, Is.EqualTo(diskToTest.Edges.B));
            Assert.That(c, Is.EqualTo(diskToTest.Edges.C));
        }

        [TestCase(1.7976931348623157E+77, 1.7976931348623157E+77, 1.7976931348623157E+77)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        [TestCase(double.MaxValue, double.MaxValue, double.MaxValue)]
        [TestCase(float.MaxValue, float.MaxValue, float.MaxValue)]
        public void TryGetArea_Should_Not_Compute<T>(T a, T b, T c) where T : IFloatingPoint<T>
        {
            //Arrange
            var triangleToTest = Triangle<T>.CreateInstance(a, b, c);

            //Act
            var isComputable = triangleToTest.TryGetArea(out var actualArea);

            //Assert
            Assert.False(isComputable);
            Assert.That(actualArea, Is.Null);
        }
    }
}