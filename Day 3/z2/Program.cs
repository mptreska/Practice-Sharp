using System;

class Program
{
    static void Main()
    {
        int n = 100;
        int[] arr = new int[n];
        Random rnd = new Random();

        Console.WriteLine("Массив:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = rnd.Next(-100, 101);
            Console.Write(arr[i] + " ");
        }
        Console.WriteLine();

        int max = arr[0], min = arr[0];
        int maxIndex = 0, minIndex = 0;

        for (int i = 1; i < n; i++)
        {
            if (arr[i] > max) { max = arr[i]; maxIndex = i; }
            if (arr[i] < min) { min = arr[i]; minIndex = i; }
        }

        int left = Math.Min(maxIndex, minIndex);
        int right = Math.Max(maxIndex, minIndex);

        double sum = 0;
        int count = 0;

        for (int i = left; i <= right; i++)
        {
            sum += arr[i];
            count++;
        }

        Console.WriteLine($"Индекс минимума: {minIndex}, Индекс максимума: {maxIndex}");
        Console.WriteLine($"Среднее арифметическое между ними: {sum / count:F4}");

        Console.WriteLine("\nСортировка:");
        Array.Sort(arr);
        for (int i = 0; i < n; i++)
            Console.Write(arr[i] + " ");
        Console.WriteLine();

        Console.Write("\nВведите k для бинарного поиска: ");
        int k = int.Parse(Console.ReadLine());

        int lo = 0, hi = n - 1, result = -1;
        while (lo <= hi)
        {
            int mid = (lo + hi) / 2;
            if (arr[mid] == k) { result = mid; break; }
            else if (arr[mid] < k) lo = mid + 1;
            else hi = mid - 1;
        }

        if (result != -1)
            Console.WriteLine($"Число {k} найдено на позиции {result}");
        else
            Console.WriteLine($"Число {k} не найдено");
    }
}