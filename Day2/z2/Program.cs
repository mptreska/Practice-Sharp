using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите трехзначное число: ");
        int n = int.Parse(Console.ReadLine());

        int c1 = n / 100;
        int c2 = (n / 10) % 10;
        int c3 = n % 10;

        bool res = (c1 < c2) && (c2 < c3);

        Console.WriteLine(res);
    }
}