using Dane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logika
{
    public abstract class Logic
    {
        public static Logic CreateAPI()
        {
            return new LogicAPI(Data.CreateAPI());
        }
        public abstract void createPlane(int width, int height, int numberOfCircles, int radius);
        public abstract List<CircleLogic> GetCircleLogics();
        
        
    }
    public class LogicAPI : Logic
    {
        private Data dataAPI;
        private List<CircleLogic> CircleLogics = new List<CircleLogic>();
        bool enabled = false;
        private Logger logger=new Logger();
        public LogicAPI(Data abstractDataAPI = null)
        {
            this.dataAPI = Data.CreateAPI();
        }
        public List<CircleLogic> circleLogics
        {
            get { return CircleLogics; }
            set { CircleLogics = value;
            }
        }

        public bool Enabled { get => enabled; set => enabled = value; }
        public override void createPlane(int width, int height, int numberOfCircles, int radius)
        {
            circleLogics.Clear();
            dataAPI.CreatePlane(width, height, numberOfCircles, radius);
            foreach (Dane.Circle circle in dataAPI.GetCircles())
            {
                this.CircleLogics.Add(new CircleLogic(circle));
            }
            this.enabled = true;
            this.logger.Circles = CircleLogics;
            Task timer = new Task(()=> { this.logger.startLogger(1000); });
            timer.Start();
            foreach (CircleLogic circleLogic in this.CircleLogics)
            {
                circleLogic.randomizeSpeed();
                Task task = new Task(() =>
                {
                    int counter = 10;
                    while (enabled)
                    {
                        if (counter <= 0)
                        {
                            checkCollisionsWithCircles(circleLogic, counter);
                        }

                        if (circleLogic.X + circleLogic.Xspeed >= (width - circleLogic.Radius))
                        {
                            circleLogic.Xspeed *= -1;
                        }

                        if (circleLogic.Y + circleLogic.Y >= (height - circleLogic.Radius) * 2)

                        {
                            circleLogic.Yspeed *= -1;
                        }
                        if (circleLogic.X + circleLogic.Xspeed <= 0)
                        {
                            circleLogic.Xspeed *= -1;
                        }
                        if (circleLogic.Y + circleLogic.Yspeed <= 0)
                        {
                            circleLogic.Yspeed *= -1;
                        }
                        circleLogic.X += circleLogic.Xspeed;
                        circleLogic.Y += circleLogic.Yspeed;
                        counter--;
                        Thread.Sleep(10);
                    }
                });
                task.Start();
            }
        }

        void checkCollisionsWithCircles(CircleLogic circle, int counter)
        {
            foreach (CircleLogic secondCircle in this.CircleLogics)
            {
                if (circle.Equals(secondCircle))
                {
                    continue;
                }

                    if (CalculateDistance(circle, secondCircle) < (circle.Radius / 2 + secondCircle.Radius / 2))
                    {
                    lock (secondCircle)
                    {
                        //tu sprawdzamy czy kulki już sie nie odbiły
                        int relativeX = secondCircle.Xspeed - circle.Xspeed;
                        int relativeY = secondCircle.Yspeed - circle.Yspeed;
                        int relativeProduct = (secondCircle.X - circle.X) * relativeX + (secondCircle.Y - circle.Y) * relativeY;
                        if (relativeProduct > 0)
                        {
                            continue;
                        }
                        //wyliczamy nowy kierunek i wartość prędkości
                        int firstSpeedX = circle.Xspeed;
                        int firstSpeedY = circle.Yspeed;
                        int secondSpeedX = secondCircle.Xspeed;
                        int secondSpeedY = secondCircle.Yspeed;

                        circle.Xspeed = ((circle.Weight - secondCircle.Weight) / (circle.Weight + secondCircle.Weight)) * firstSpeedX
                            + (2 * secondCircle.Weight) / (circle.Weight + secondCircle.Weight) * secondSpeedX;
                        circle.Yspeed = ((circle.Weight - secondCircle.Weight) / (circle.Weight + secondCircle.Weight)) * firstSpeedY
                            + (2 * secondCircle.Weight) / (circle.Weight + secondCircle.Weight) * secondSpeedY;
                        secondCircle.Xspeed = ((2 * secondCircle.Weight) / (circle.Weight + secondCircle.Weight)) * firstSpeedX
                            + ((circle.Weight - secondCircle.Weight) / (circle.Weight + secondCircle.Weight)) * secondSpeedX;
                        secondCircle.Yspeed = ((2 * secondCircle.Weight) / (circle.Weight + secondCircle.Weight)) * firstSpeedY
                            + ((circle.Weight - secondCircle.Weight) / (circle.Weight + secondCircle.Weight)) * secondSpeedY;



                    }
                }
            }
        }

        private double CalculateDistance(CircleLogic first, CircleLogic second)
        {
            return Math.Sqrt(Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2));
        }


        public override List<CircleLogic> GetCircleLogics()
        {
            return this.CircleLogics;
        }
    }
}
