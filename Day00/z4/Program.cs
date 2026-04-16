using System;

namespace PracticeTasks
{
    class MathDialog
    {
        static void Main()
        {
            Console.Write("Введите число a: ");
            double a = double.Parse(Console.ReadLine());

            Console.Write("Введите число b: ");
            double b = double.Parse(Console.ReadLine());

            Console.Write("Введите число c: ");
            double c = double.Parse(Console.ReadLine());

            Console.WriteLine("({0:F4}+({1:F4}+{2:F4}))=({0:F4}+{2:F4}+{1:F4})", a, b, c);
        }
    }
}