using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class Data
    {
        public abstract void CreatePlane (int width, int height, int numberOfCircles, int radius);
        public abstract Plane GetPlane();
        public abstract List<Circle> GetCircles();
        public static Data CreateAPI()
        {
            return new DataAPI();
        }
        internal class DataAPI : Data
        {
            private Plane plane;
            public override void CreatePlane(int width, int height, int numberOfCircles, int radius)
            {
                this.plane = new Plane(width, height);
                this.plane.spawnCircles(numberOfCircles, radius);
            }
            public override Plane GetPlane() { return this.plane; }
            public override List<Circle> GetCircles() { return this.plane.getCircleList; }
        }
    }
    
}
