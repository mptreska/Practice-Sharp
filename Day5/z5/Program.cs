using System;

abstract class Computer
{
    public abstract void Start();

    public virtual void Shutdown()
    {
        Console.WriteLine("Computer is shutting down");
    }
}

class Desktop : Computer
{
    public override void Start()
    {
        Console.WriteLine("Desktop is starting");
    }

    public override void Shutdown()
    {
        Console.WriteLine("Desktop is shutting down");
    }
}

class Laptop : Computer
{
    public override void Start()
    {
        Console.WriteLine("Laptop is starting");
    }

    public override void Shutdown()
    {
        Console.WriteLine("Laptop is shutting down");
    }
}

class Program
{
    static void Main()
    {
        Computer desktop = new Desktop();
        Computer laptop = new Laptop();

        desktop.Start();
        desktop.Shutdown();

        laptop.Start();
        laptop.Shutdown();
    }
}