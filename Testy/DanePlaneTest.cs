using Dane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testy
{
    [TestClass]
    public class PlaneTests
    {
        [TestMethod]
        public void SpawnSpheres_AddsSpheresToSphereList()
        {
            // Arrange
            int width = 800;
            int height = 600;
            int numberOfSpheres = 10;
            Plane plane = new Plane(width, height);

            // Act
            plane.spawnSpheres(numberOfSpheres);

            // Assert
            Assert.AreEqual(numberOfSpheres, plane.getSphereList.Count);
        }

        [TestMethod]
        public void CheckIfPointOnPlane_ReturnsTrueForPointOnPlane()
        {
            // Arrange
            int width = 800;
            int height = 600;
            Plane plane = new Plane(width, height);

            // Act
            bool result = plane.checkIfPointOnPlane(400, 300);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckIfPointOnPlane_ReturnsFalseForPointOffPlane()
        {
            // Arrange
            int width = 800;
            int height = 600;
            Plane plane = new Plane(width, height);

            // Act
            bool result = plane.checkIfPointOnPlane(900, 700);

            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void widthHightEnabledTest()
        {
            Plane plane = new Plane(200, 100);
            Assert.AreEqual(plane.Width, 200);
            Assert.AreEqual(plane.Height, 100);
            Assert.AreEqual(plane.enabled, true);

        }
    }
}