using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TDDSolidos;

namespace TDDSolidosUnitTest
{
    public class UnitTestCylinder
    {
        [Fact]
        public void TestCylinderSurfaceArea()
        {
            // Arrange
            Cylinder c = new Cylinder(5, 20);
            double expectedArea = 78.54;

            // Act
            double realArea = Math.Round(c.SurfaceArea(), 2); 

            // Assert
            Assert.Equal(expectedArea, realArea);
        }

        [Fact]
        public void TestCylinderVolume()
        {
            // Arrange
            Cylinder c = new Cylinder(5, 20);
            double expectedVolume = 1570.8;

            // Act
            double realVolume = Math.Round(c.Volume(), 2);

            // Assert
            Assert.Equal(expectedVolume, realVolume);
        }
    }
}
