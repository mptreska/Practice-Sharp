using System;

class Program
{
    static bool IsLeapYear(int y)
    {
        return (y % 4 == 0 && y % 100 != 0) || (y % 400 == 0);
    }

    static int DaysInYear(int y)
    {
        return IsLeapYear(y) ? 366 : 365;
    }

    static void Main()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.Write($"Введите год {i + 1}: ");
            int y = int.Parse(Console.ReadLine());
            Console.WriteLine($"Количество дней в {y} году: {DaysInYear(y)}");
        }
    }
}