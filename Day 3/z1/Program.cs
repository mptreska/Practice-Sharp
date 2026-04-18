using System;

class Program
{
    static void Main()
    {
        int n = 15;
        int[] arr = new int[n];
        Random rnd = new Random();

        Console.WriteLine("Массив:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = rnd.Next(-10, 11);
            Console.Write(arr[i] + " ");
        }
        Console.WriteLine();

        int count = 0;
        Console.WriteLine("Положительные элементы:");
        for (int i = 0; i < n; i++)
        {
            if (arr[i] > 0)
            {
                Console.Write(arr[i] + " ");
                count++;
            }
        }
        Console.WriteLine();
        Console.WriteLine("Количество положительных элементов: " + count);
    }
}