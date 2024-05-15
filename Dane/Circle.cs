using System;
using System.ComponentModel;


namespace Dane
{
   public class Circle : INotifyPropertyChanged {
        public double X { get; set; }
        public double Y { get; set; }
        public int Size { get; set; }
        public int Radius { get; set; }
        public double dirX { get; set; }
        public double dirY { get; set; }

        public double Speed { get; set; }

        public double Mass { get; set; }

        public Circle(int x, int y, int size, int radius, double speed, double mass) {
            X = x;
            Y = y;
            Size = size;
            Radius = radius;
            Speed = speed;
            Mass = mass;

            this.dirX = 0;
            this.dirY = 0;


            while (this.dirX == 0 && this.dirY == 0) {
                Random random = new Random();
                this.dirX = random.NextDouble();
                this.dirY = random.NextDouble();
            }

            this.dirX *= speed/ mass; 
            this.dirY *= speed/ mass;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        static void Main() {

        }
    }


}
