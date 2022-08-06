using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TDDSolidos;

namespace TDDSolidosUnitTest
{
    public class UnitTestSphere
    {
        [Fact]
        public void TestSphereSurfaceArea()
        {
            // Arrange
            Sphere c = new Sphere(5);
            double expectedArea = 78.54;

            // Act
            double realArea = Math.Round(c.SurfaceArea(), 2);

            // Assert
            Assert.Equal(expectedArea, realArea);
        }

        [Fact]
        public void TestSphereVolume()
        {
            // Arrange
            Sphere c = new Sphere(5);
            double expectedVolume = 523.6;

            // Act
            double realVolume = Math.Round(c.Volume(), 2);

            // Assert
            Assert.Equal(expectedVolume, realVolume);
        }
    }
}
