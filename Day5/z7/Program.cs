using System;

class Converter
{
    public static string ToString(int number)
    {
        return number.ToString();
    }

    public static string ToString(double number)
    {
        return number.ToString();
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите целое число: ");
        int intNum = int.Parse(Console.ReadLine());

        Console.Write("Введите вещественное число: ");
        double doubleNum = double.Parse(Console.ReadLine());

        Console.WriteLine($"ToString({intNum}) = {Converter.ToString(intNum)}");
        Console.WriteLine($"ToString({doubleNum}) = {Converter.ToString(doubleNum)}");
    }
}