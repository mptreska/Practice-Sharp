using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите A: ");
        int A = int.Parse(Console.ReadLine());

        Console.Write("Введите B: ");
        int B = int.Parse(Console.ReadLine());

        if (A > B)
        {
            Console.WriteLine("Ошибка: A должно быть меньше или равно B");
            return;
        }

        if (B <= 0)
        {
            Console.WriteLine("В диапазоне нет положительных чисел");
            return;
        }

        int start = A < 1 ? 1 : A;

        for (int i = start; i <= B; i++)
        {
            Console.WriteLine(i);
        }
    }
}