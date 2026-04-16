using System;

namespace PracticeTasks
{
    class NumberDigits
    {
        static void Main()
        {
            Console.Write("Введите трехзначное число: ");
            int number = int.Parse(Console.ReadLine());

            int firstDigit = number / 100;
            int lastDigit = number % 10;

            int product = firstDigit * lastDigit;

            Console.WriteLine("Произведение первой и последней цифр: {0}", product);
        }
    }
}