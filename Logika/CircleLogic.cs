using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

public class CircleLogic : INotifyPropertyChanged
{
    private Dane.Circle circle;
    private const int MIN_SPEED = 1; // Minimalna prêdkoœæ, aby pi³ki siê nie zatrzymywa³y

    public CircleLogic(Dane.Circle circle)
    {
        this.circle = circle;
    }

    public int Radius
    {
        get { return circle.Radius; }
        set
        {
            circle.Radius = value;
            OnPropertyChanged(nameof(Radius));
        }
    }

    public int X
    {
        get { return circle.X; }
        set
        {
            circle.X = value;
            OnPropertyChanged(nameof(X));
        }
    }

    public int Y
    {
        get { return circle.Y; }
        set
        {
            circle.Y = value;
            OnPropertyChanged(nameof(Y));
        }
    }

    public int Xspeed
    {
        get { return circle.Xspeed; }
        set
        {
            circle.Xspeed = value;
            OnPropertyChanged(nameof(Xspeed));
        }
    }

    public int Yspeed
    {
        get { return circle.Yspeed; }
        set
        {
            circle.Yspeed = value;
            OnPropertyChanged(nameof(Yspeed));
        }
    }

    public int Weight
    {
        get { return circle.Weight; }
        set { circle.Weight = value; }
    }

    public void randomizeSpeed()
    {
        Random random = new Random();
        this.Xspeed = random.Next(-3, 3);
        this.Yspeed = random.Next(-3, 3);
        // Zapewniamy, ¿e prêdkoœci nie s¹ zerowe
        if (this.Xspeed == 0) this.Xspeed = MIN_SPEED;
        if (this.Yspeed == 0) this.Yspeed = MIN_SPEED;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
