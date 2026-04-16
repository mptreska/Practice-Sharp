using System;

namespace PracticeTasks
{
    class ComplexFormulas
    {
        static void Main()
        {
            Console.Write("Введите m: ");
            double m = double.Parse(Console.ReadLine());
            Console.Write("Введите n: ");
            double n = double.Parse(Console.ReadLine());

            double numeratorZ1 = (m - 1) * Math.Sqrt(m) - (n - 1) * Math.Sqrt(n);
            double denominatorZ1 = Math.Sqrt(Math.Pow(m, 3) * n + n * m + Math.Pow(m, 2) - m);

            double z1 = numeratorZ1 / denominatorZ1;
            double z2 = (Math.Sqrt(m) - Math.Sqrt(n)) / m;

            Console.WriteLine("Результат z1 = {0:G}", z1);
            Console.WriteLine("Результат z2 = {0:G}", z2);
        }
    }
}