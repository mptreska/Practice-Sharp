using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите сторону a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Введите сторону b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Введите сторону c: ");
        double c = double.Parse(Console.ReadLine());

        if (a + b > c && a + c > b && b + c > a)
        {
            if (a == b || a == c || b == c)
                Console.WriteLine("Треугольник является равнобедренным");
            else
                Console.WriteLine("Треугольник не является равнобедренным");
        }
        else
        {
            Console.WriteLine("Треугольник с такими сторонами не существует");
        }
    }
}