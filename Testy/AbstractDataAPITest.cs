using Dane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testy
{
    [TestClass]
    public class AbstractDataAPITests
    {
        [TestMethod]
        public void TestCreatePlane()
        {
            // arrange
            Data dataAPI = Data.CreateAPI();
            int width = 500;
            int height = 500;
            int numberOfSpheres = 10;

            // act
            dataAPI.CreatePlane(width, height, numberOfSpheres);
            Plane plane = dataAPI.GetPlane();
            List<Circle> spheres = dataAPI.GetSpheres();

            // assert
            Assert.AreEqual(width, plane.Width);
            Assert.AreEqual(height, plane.Height);
            Assert.AreEqual(numberOfSpheres, spheres.Count);
        }
    }
}
