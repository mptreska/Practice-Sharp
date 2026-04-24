using System;

delegate double OrderHandler(double amount);

class OrderProcessor
{
    public static double ApplyDiscount(double amount)
    {
        double discount = amount * 0.1;
        Console.WriteLine($"Скидка 10%: -{discount} руб.");
        return amount - discount;
    }

    public static double CalculateTax(double amount)
    {
        double tax = amount * 0.2;
        Console.WriteLine($"Налог 20%: +{tax} руб.");
        return amount + tax;
    }

    public static void HandleOrder(double amount, OrderHandler handler)
    {
        Console.WriteLine($"Сумма заказа: {amount} руб.");
        double result = handler(amount);
        Console.WriteLine($"Итоговая сумма: {result} руб.");
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите сумму заказа: ");
        double amount = double.Parse(Console.ReadLine());

        Console.WriteLine("\nОбработка со скидкой:");
        OrderProcessor.HandleOrder(amount, OrderProcessor.ApplyDiscount);

        Console.WriteLine("\nОбработка с налогом:");
        OrderProcessor.HandleOrder(amount, OrderProcessor.CalculateTax);
    }
}