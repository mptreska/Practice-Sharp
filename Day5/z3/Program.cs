using System;

class Program
{
    static bool IsSorted(int[] arr, int index = 0)
    {
        if (arr.Length <= 1 || index == arr.Length - 1)
            return true;

        if (arr[index] > arr[index + 1])
            return false;

        return IsSorted(arr, index + 1);
    }

    static void Main()
    {
        Console.Write("Введите элементы массива через пробел: ");
        string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int[] arr = new int[input.Length];

        for (int i = 0; i < input.Length; i++)
            arr[i] = int.Parse(input[i]);

        Console.WriteLine(IsSorted(arr));
    }
}