using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите номер карты (6-14): ");
        int k = int.Parse(Console.ReadLine());

        if (k < 6 || k > 14)
        {
            Console.WriteLine("Некорректный номер карты");
            return;
        }

        switch (k)
        {
            case 14:
                Console.WriteLine("Туз");
                break;
            case 13:
                Console.WriteLine("Король");
                break;
            case 12:
                Console.WriteLine("Дама");
                break;
            case 11:
                Console.WriteLine("Валет");
                break;
            case 10:
                Console.WriteLine("Десятка");
                break;
            case 9:
                Console.WriteLine("Девятка");
                break;
            case 8:
                Console.WriteLine("Восьмерка");
                break;
            case 7:
                Console.WriteLine("Семерка");
                break;
            case 6:
                Console.WriteLine("Шестерка");
                break;
        }
    }
}
