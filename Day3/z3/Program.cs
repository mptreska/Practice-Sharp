using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите N (размер матрицы, N<10): ");
        int N = int.Parse(Console.ReadLine());

        Console.Write("Введите a (минимум): ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Введите b (максимум): ");
        int b = int.Parse(Console.ReadLine());

        int[,] matrix = new int[N, N];
        Random rnd = new Random();

        Console.WriteLine("Матрица:");
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                matrix[i, j] = rnd.Next(a, b + 1);
                Console.Write($"{matrix[i, j],5}");
            }
            Console.WriteLine();
        }

        Console.Write("Введите K: ");
        int K = int.Parse(Console.ReadLine());

        Console.Write("Введите L: ");
        int L = int.Parse(Console.ReadLine());

        int sum = 0;
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                if (matrix[i, j] >= K && matrix[i, j] < L)
                    sum += matrix[i, j];

        Console.WriteLine($"Сумма чисел из промежутка [{K}, {L}): {sum}");

        Console.Write("Введите номер столбца (0 - " + (N - 1) + "): ");
        int col = int.Parse(Console.ReadLine());

        int maxCol = matrix[0, col];
        for (int i = 1; i < N; i++)
            if (matrix[i, col] > maxCol)
                maxCol = matrix[i, col];

        Console.WriteLine($"Наибольший элемент {col}-го столбца: {maxCol}");
    }
}