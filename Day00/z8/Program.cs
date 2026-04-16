using System;

namespace PracticeTasks
{
    class MathExpression
    {
        static void Main()//тоже самое что и в z6
        {
            Console.Write("Введите x: ");
            double x = double.Parse(Console.ReadLine());

            double term1 = 2 * Math.Atan(Math.Sqrt(1 - Math.Pow(x, 2)));
            double term2 = Math.Log(7 * x) / (1 + Math.Exp(x));

            double y = term1 + term2;

            Console.WriteLine("Результат y = {0:F4}", y);
        }
    }
}