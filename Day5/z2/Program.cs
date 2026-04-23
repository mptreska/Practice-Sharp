using System;

class Program
{
    static void SortInc3(ref double A, ref double B, ref double C)
    {
        double t;

        if (A > B)
        {
            t = A;
            A = B;
            B = t;
        }

        if (B > C)
        {
            t = B;
            B = C;
            C = t;
        }

        if (A > B)
        {
            t = A;
            A = B;
            B = t;
        }
    }

    static void Main()
    {
        Console.Write("Введите A1: ");
        double A1 = double.Parse(Console.ReadLine());

        Console.Write("Введите B1: ");
        double B1 = double.Parse(Console.ReadLine());

        Console.Write("Введите C1: ");
        double C1 = double.Parse(Console.ReadLine());

        Console.Write("Введите A2: ");
        double A2 = double.Parse(Console.ReadLine());

        Console.Write("Введите B2: ");
        double B2 = double.Parse(Console.ReadLine());

        Console.Write("Введите C2: ");
        double C2 = double.Parse(Console.ReadLine());

        SortInc3(ref A1, ref B1, ref C1);
        SortInc3(ref A2, ref B2, ref C2);

        Console.WriteLine($"Первый набор: {A1} {B1} {C1}");
        Console.WriteLine($"Второй набор: {A2} {B2} {C2}");
    }
}