using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите целое число N (N > 0): ");
        int N = int.Parse(Console.ReadLine());
        int sum = 0;
        for (int i = N; i <= 2 * N; i++)
        {
            sum += i * i;
        }
        Console.WriteLine("Сумма квадратов от " + N + "^2 до " + (2 * N) + "^2 = " + sum);
    }
}