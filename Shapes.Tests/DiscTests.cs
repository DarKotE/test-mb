using NUnit.Framework;
using Shapes.Primitives;

namespace Shapes.Tests
{
    [TestFixture]
    public class DiscTests
    {
        [TestCase(-1.0)]
        [TestCase(-float.Epsilon)]
        [TestCase(double.MinValue)]
        [TestCase(double.NaN)]
        [TestCase(double.NegativeInfinity)]
        public void CreateInstance_Should_Throw(double testValue)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Disk<float>.CreateInstance((float)testValue));
            Assert.Throws<ArgumentOutOfRangeException>(() => Disk<double>.CreateInstance(testValue));
        }

        [TestCase(0)]
        [TestCase(1.0)]
        [TestCase(Math.PI)]
        [TestCase(float.Epsilon)]
        [TestCase(double.MaxValue)]
        [TestCase(double.PositiveInfinity)]
        public void CreateInstance_Should_Not_Throw(double testValue)
        {
            Assert.DoesNotThrow(() => Disk<float>.CreateInstance((float)testValue));
            Assert.DoesNotThrow(() => Disk<double>.CreateInstance(testValue));
        }

        [TestCase(10.0f, 314.15926535897931f)]
        [TestCase(10.0d, 314.15926535897931d)]
        [TestCase(1d, Math.PI)]
        [TestCase(0d, 0d)]
        public void TryGetArea_Should_Compute<T>(T radius, T expectedAreaF) where T : IFloatingPoint<T>
        {
            //Arrange
            var diskToTest = Disk<T>.CreateInstance(radius);
            var expectedArea = new Area<T>(expectedAreaF);

            //Act
            var isComputable = diskToTest.TryGetArea(out var actualArea);

            //Assert
            Assert.True(isComputable);
            Assert.That(actualArea, Is.EqualTo(expectedArea));
            Assert.That(radius, Is.EqualTo(diskToTest.Radius));
        }

        [TestCase(1.7976931348623157E+154)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.MaxValue)]
        [TestCase(float.MaxValue)]

        public void TryGetArea_Should_Not_Compute<T>(T radius) where T : IFloatingPoint<T>
        {
            //Arrange
            var diskToTest = Disk<T>.CreateInstance(radius);

            //Act
            var isComputable = diskToTest.TryGetArea(out var actualArea);

            //Assert
            Assert.False(isComputable);
            Assert.That(actualArea, Is.Null);
        }
    }
}