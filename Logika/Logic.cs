using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Dane;

namespace Logika
{
    public class Logic
    {
        public static CircleList circleList = new CircleList();
        private static Logger logger = new Logger("\\diagnostic_log.txt");
        public static void CreateCircles(Canvas canvas, int value, int radius)
        {
            if (radius == 0)
            {
                radius = 1;
            }
            else
            {
                if (radius < 0)
                {
                    radius -= radius * 2;
                }
                if (radius > 60)
                {
                    radius = 60;
                }
            }

            for (int i = 0; i < value; i++)
            {
                CreateCircle(canvas, radius, radius);
            }
        }

        public static void CreateCircle(Canvas canvas, int size, int radius)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());

            int x = r.Next(80, (int)canvas.Width - 20);
            int y = r.Next(80, (int)canvas.Height - 20);
            double speed = 0;

            while (speed < 60)
            {
                Random random = new Random();
                speed = random.NextDouble() * 80; // Zmienione, aby zawsze zwracać różną wartość
            }

            if (radius < 10)
            {
                radius = 10;
            }

            Circle circleObject = new Circle(x, y, size, radius, speed, radius);
            Random randomc = new Random();
            Color color = Color.FromRgb((byte)randomc.Next(256), (byte)randomc.Next(256), (byte)randomc.Next(256));
            Ellipse circle = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Fill = new SolidColorBrush(color)
            };

            Canvas.SetLeft(circle, x - radius);
            Canvas.SetTop(circle, y - radius);

            canvas.Children.Add(circle);

            circleList.AddCircle(circleObject);
            Task task = new Task(async () =>
            {
                while (true)
                {
                    var dispatcher = Application.Current.Dispatcher;

                    dispatcher.Invoke(() =>
                    {
                        double left = Canvas.GetLeft(circle);
                        double top = Canvas.GetTop(circle);

                        if (left + circle.Width >= canvas.Width)
                        {
                            circleObject.dirX = -circleObject.dirX;
                        }
                        else if (left <= 0)
                        {
                            circleObject.dirX = -circleObject.dirX;
                        }

                        if (top + circle.Height >= canvas.Height)
                        {
                            circleObject.dirY = -circleObject.dirY;
                        }
                        else if (top <= 0)
                        {
                            circleObject.dirY = -circleObject.dirY;
                        }

                        circleObject.X += circleObject.dirX;
                        circleObject.Y += circleObject.dirY;

                        Canvas.SetLeft(circle, circleObject.X - circleObject.Radius);
                        Canvas.SetTop(circle, circleObject.Y - circleObject.Radius);

                        foreach (var otherCircle in circleList)
                        {
                            if (otherCircle != circleObject)
                            {
                                double dx = circleObject.X - otherCircle.X;
                                double dy = circleObject.Y - otherCircle.Y;
                                double distance = Math.Sqrt(dx * dx + dy * dy);

                                if (distance < (circleObject.Radius + otherCircle.Radius))
                                {
                                    double normalX = dx / distance;
                                    double normalY = dy / distance;
                                    double velocityX = circleObject.dirX - otherCircle.dirX;
                                    double velocityY = circleObject.dirY - otherCircle.dirY;
                                    double velocityAlongNormal = velocityX * normalX + velocityY * normalY;

                                    if (velocityAlongNormal > 0)
                                    {
                                        break;
                                    }

                                    double e = 1;
                                    double j = -(1 + e) * velocityAlongNormal;
                                    j /= (1 / circleObject.Mass + 1 / otherCircle.Mass);

                                    circleObject.dirX -= -j * normalX / circleObject.Mass;
                                    circleObject.dirY -= -j * normalY / circleObject.Mass;
                                    otherCircle.dirX += -j * normalX / otherCircle.Mass;
                                    otherCircle.dirY += -j * normalY / otherCircle.Mass;

                                    /*double maxSpeed = 8;
                                    double speed1 = Math.Sqrt(circleObject.dirX * circleObject.dirX + circleObject.dirY * circleObject.dirY);
                                    double speed2 = Math.Sqrt(otherCircle.dirX * otherCircle.dirX + otherCircle.dirY * otherCircle.dirY);
                                    if (speed1 > maxSpeed)
                                    {
                                        circleObject.dirX *= maxSpeed / speed1;
                                        circleObject.dirY *= maxSpeed / speed1;
                                    }
                                    if (speed2 > maxSpeed)
                                    {
                                        otherCircle.dirX *= maxSpeed / speed2;
                                        otherCircle.dirY *= maxSpeed / speed2;
                                    }*/

                                    break;
                                }
                            }
                        }
                    });

                    await logger.LogAsync(circleObject.ToJson());
                    await Task.Delay(1); // Zmieniony czas opóźnienia na 1 ms
                }
            });

            task.Start();

        }

        static void Main() { }
    }
}
