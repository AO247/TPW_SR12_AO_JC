using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public class Plane
    {
        private int width;
        private int height;
        private bool visibility = true;
        private List<Circle> circleList = new List<Circle>();

        public Plane(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public bool enabled { get => visibility; set => visibility = value; }
        public List<Circle> getCircleList { get => circleList; }

        public void spawnCircles(int numberOfCircles, int radius)
        {
            Random random = new Random();
            int x, y;
            for(int i = 0; i < numberOfCircles; i++)
            {
                do
                {
                    x = random.Next(radius, this.width - radius);
                    y = random.Next(radius, this.height - radius);
                }
                while (!checkIfPointOnPlane(x, y));
                circleList.Add(new Circle(x,y,radius));
            }
        }

        public bool checkIfPointOnPlane(double x, double y)
        {
            if (x >= 0 && x <= width && y >= 0 && y <= height) return true;
            return false;
        }

    }
}
