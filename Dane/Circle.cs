using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Dane
{
    public class Circle : INotifyPropertyChanged
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Size { get; set; }
        public int Radius { get; set; }
        public double dirX { get; set; }
        public double dirY { get; set; }
        public double Speed { get; set; }
        public double Mass { get; set; }

        public Circle(int x, int y, int size, int radius, double speed, double mass)
        {
            X = x;
            Y = y;
            Size = size;
            Radius = radius;
            Speed = speed;
            Mass = mass;

            Random random = new Random();

            while (dirX == 0 && dirY == 0)
            {
                dirX = random.NextDouble() * 2 - 1;
                dirY = random.NextDouble() * 2 - 1;
            }

            dirX *= speed / mass;
            dirY *= speed / mass;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        static void Main() { }
    }
}
