using System;

namespace PracticeTasks
{
    class NumberPermutation
    {
        static void Main()
        {
            Console.Write("Введите четырехзначное число: ");
            int number = int.Parse(Console.ReadLine());

            int digit1 = number / 1000;
            int digit2 = (number / 100) % 10;
            int digit3 = (number / 10) % 10;
            int digit4 = number % 10;

            int result = digit2 * 1000 + digit1 * 100 + digit4 * 10 + digit3;

            Console.WriteLine("Результат перестановки: {0}", result);
        }
    }
}