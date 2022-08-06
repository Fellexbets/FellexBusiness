using System;
using Xunit;
using TDDSolidos;

namespace TDDSolidosUnitTest
{
    public class UnitTestCube
    {
        [Fact]
        public void TestCubeSurfaceArea()
        {
            // Arrange
            Cube c = new Cube(3);
            double expectedArea = 9;

            // Act
            double realArea = Math.Round(c.SurfaceArea(), 2);

            // Assert
            Assert.Equal(expectedArea, realArea);
        }

        [Fact]
        public void TestCubeVolume()
        {
            // Arrange
            Cube c = new Cube(3);
            double expectedVolume = 27;

            // Act
            double realVolume = Math.Round(c.Volume(), 2);

            // Assert
            Assert.Equal(expectedVolume, realVolume);
        }
    }
}
