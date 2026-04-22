using System;

static class MathProcessor
{
    public static double CalculateExpression(double a, double b)
    {
        return (3 * b - 2 / (a * a)) / 4;
    }

    public static double CubeOfQuotient(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("b не может быть равно нулю");
        return Math.Pow(a / b, 3);
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Введите b: ");
        double b = double.Parse(Console.ReadLine());

        Console.WriteLine($"Значение выражения (3b - 2/a²) / 4 = {MathProcessor.CalculateExpression(a, b):F4}");
        Console.WriteLine($"Куб частного a/b = {MathProcessor.CubeOfQuotient(a, b):F4}");
    }
}