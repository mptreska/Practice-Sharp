using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите A: ");
        int A = int.Parse(Console.ReadLine());

        Console.Write("Введите B: ");
        int B = int.Parse(Console.ReadLine());

        if (A >= B)
        {
            Console.WriteLine("Ошибка: должно выполняться A < B");
            return;
        }

        int sum = 0;

        for (int i = A; i <= B; i++)
        {
            sum += i;
        }

        Console.WriteLine("Сумма = " + sum);
    }
}