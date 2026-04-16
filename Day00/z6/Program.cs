using System;

namespace PracticeTasks
{
    class MathExpression
    {
        static void Main()
        {
            Console.Write("Введите x: ");
            double x = double.Parse(Console.ReadLine());

            double part1 = 2 * Math.Atan(Math.Sqrt(1 - Math.Pow(x, 2)));
            double part2 = Math.Log(7 * x) / (1 + Math.Exp(x));

            double y = part1 + part2;

            Console.WriteLine("При x = {0}, y = {1:F4}", x, y);
        }
    }
}