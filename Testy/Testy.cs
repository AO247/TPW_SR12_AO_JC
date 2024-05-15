using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Testy {
    [TestClass]
    public class Testy {
        [TestMethod]
        public void TestMethod1() {
            Ellipse circle = new Ellipse
            {
                Width = 2,
                Height = 2
            };
            Assert.AreEqual(circle.Width, 2);
            Assert.AreEqual(circle.Height, 2);
        }
        [TestMethod]
        public void SpawnCircle() {
            Canvas canvas = new Canvas
            {
                Width = 900,
                Height = 900
            };
            Logika.Logic.CreateCircles(canvas, 5, 5);
            Process currentProcess2 = Process.GetCurrentProcess();
            int threadCount2 = currentProcess2.Threads.Count;
            Debug.WriteLine(threadCount2);
            Assert.AreEqual(5, Logika.Logic.circleList.CountCircles());
            Assert.IsTrue(threadCount2 < 50);
        }

        [TestMethod]
        public void CircleList() {
            Dane.CircleList list = new Dane.CircleList();
            Dane.Circle circle1 = new Dane.Circle(5, 5, 5, 5, 10, 1);
            list.AddCircle(circle1);
            Assert.AreEqual(list.CountCircles(), 1);
            Assert.AreEqual(list.GetCircle(0), circle1);
            list.Clear();
            Assert.AreEqual(list.CountCircles(), 0);
        }

        [TestMethod]
        public void Circle() {
            Dane.Circle circle1 = new Dane.Circle(5, 5, 5, 5, 10, 1);
            Assert.AreEqual(circle1.X, 5);
            Assert.AreEqual(circle1.Y, 5);  
            Assert.AreEqual(circle1.Size, 5);
            Assert.AreEqual(circle1.Radius, 5);
            Assert.AreEqual(circle1.Speed, 10);
            Assert.AreEqual(circle1.Mass, 1);
        }
    }
}
