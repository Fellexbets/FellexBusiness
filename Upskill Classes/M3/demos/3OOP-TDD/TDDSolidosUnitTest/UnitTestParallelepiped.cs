using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TDDSolidos;

namespace TDDSolidosUnitTest
{
    public class UnitTestParallelepiped
    {
        [Fact]
        public void TestParallelepipedSurfaceArea()
        {
            // Arrange
            Parallelepiped c = new Parallelepiped(2, 3, 4);
            double expectedArea = 6;

            // Act
            double realArea = Math.Round(c.SurfaceArea(), 2);

            // Assert
            Assert.Equal(expectedArea, realArea);
        }

        [Fact]
        public void TestParallelepipedVolume()
        {
            // Arrange
            Parallelepiped c = new Parallelepiped(2, 3, 4);
            double expectedVolume = 24;

            // Act
            double realVolume = Math.Round(c.Volume(), 2);

            // Assert
            Assert.Equal(expectedVolume, realVolume);
        }
    }
}
