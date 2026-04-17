using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите сторону a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Введите сторону b: ");
        double b = double.Parse(Console.ReadLine());

        double p = 2 * (a + b);
        double d = Math.Sqrt(a * a + b * b);

        Console.WriteLine("Периметр: " + p);
        Console.WriteLine("Диагональ: " + d);

        
        Console.ReadKey();
    }
}