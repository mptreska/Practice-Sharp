using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите A: ");
        double A = double.Parse(Console.ReadLine());

        Console.Write("Введите B: ");
        double B = double.Parse(Console.ReadLine());

        Console.Write("Введите M: ");
        int M = int.Parse(Console.ReadLine());

        if (A >= B || M <= 0)
        {
            Console.WriteLine("Некорректные данные");
            return;
        }

        double H = (B - A) / M;
        double x = A;

        for (int i = 0; i <= M; i++)
        {
            double y = Math.Pow(x, 2) - Math.Exp(x);
            Console.WriteLine($"x = {x:F4}  y = {y:F4}");
            x += H;
        }
    }
}