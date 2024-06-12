using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Dane
{
    public class Circle : INotifyPropertyChanged
    {
        private int x;
        private int y;
        private int weight=10;
        private int radius;
        private int xspeed;
        private int yspeed;
       

        public Circle(int x, int y, int radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        public int Yspeed
        {
            get => yspeed;
            set
            {
                if (value.Equals(Yspeed)) return;
                yspeed = value;
                OnPropertyChanged("YSpeed");
            }
        }

        public int Xspeed
        {
            get => xspeed;
            set
            {
                if (value.Equals(Xspeed)) return;
                xspeed = value;
                OnPropertyChanged("XSpeed");
            }
        }

        public int Radius
        {
            get => radius;
            set
            {
                if (value.Equals(radius)) return;
                radius = value;
                OnPropertyChanged("Radius");
            }
        }

        public int Y
        {
            get => y;
            set
            {
                if (value.Equals(y)) return;
                y = value;
                OnPropertyChanged("Y");
            }
        }

        public int X
        {
            get => x;
            set
            {
                if (value.Equals(x)) return;
                x = value;
                OnPropertyChanged("X");
            }
        }
        public int Weight
        {
            get { return weight; }
            set { if (value <= 0) return; weight = value; }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}