using System;

class Program
{
    static bool IsPrime(int n)
    {
        if (n < 2) return false;
        for (int i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0) return false;
        return true;
    }

    static void Main()
    {
        int rows = 5;
        int[][] jagged = new int[rows][];
        Random rnd = new Random();

        Console.WriteLine("Ступенчатый массив:");
        for (int i = 0; i < rows; i++)
        {
            jagged[i] = new int[i + 1];
            for (int j = 0; j <= i; j++)
            {
                jagged[i][j] = rnd.Next(1, 10);
                Console.Write($"{jagged[i][j]} ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nСтроки, сумма которых является простым числом:");
        bool found = false;

        for (int i = 0; i < rows; i++)
        {
            int sum = 0;
            for (int j = 0; j < jagged[i].Length; j++)
                sum += jagged[i][j];

            if (IsPrime(sum))
            {
                Console.Write($"Строка {i} (сумма = {sum}): ");
                for (int j = 0; j < jagged[i].Length; j++)
                    Console.Write($"{jagged[i][j]} ");
                Console.WriteLine();
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("Строк с простой суммой не найдено");
    }
}