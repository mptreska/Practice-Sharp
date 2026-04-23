using System;

abstract class Ticket
{
    public string Type { get; set; }
    public abstract double GetPrice();

    public override string ToString()
    {
        return $"Тип: {Type}, Цена: {GetPrice()} руб.";
    }
}

class Standard : Ticket
{
    public Standard() { Type = "Стандартный"; }
    public override double GetPrice() { return 300; }
}

class VIP : Ticket
{
    public VIP() { Type = "VIP"; }
    public override double GetPrice() { return 800; }
}

class Student : Ticket
{
    public Student() { Type = "Студенческий"; }
    public override double GetPrice() { return 150; }
}

class Program
{
    static void Main()
    {
        Ticket[] tickets = new Ticket[]
        {
            new Standard(),
            new VIP(),
            new Student(),
            new Standard(),
            new VIP()
        };

        Console.WriteLine("Список билетов:");
        for (int i = 0; i < tickets.Length; i++)
            Console.WriteLine(tickets[i]);
    }
}