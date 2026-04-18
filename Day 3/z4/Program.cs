using System;

class Program
{
    static void Main()
    {
        int floors = 12;
        int apartments = 4;
        int[,] house = new int[floors, apartments];
        Random rnd = new Random();

        Console.WriteLine("Информация о жильцах:");
        for (int i = 0; i < floors; i++)
        {
            Console.Write($"Этаж {i + 1}: ");
            for (int j = 0; j < apartments; j++)
            {
                house[i, j] = rnd.Next(1, 6);
                Console.Write($"Кв{j + 1}:{house[i, j]}  ");
            }
            Console.WriteLine();
        }

        int sum3 = 0, sum5 = 0;
        for (int j = 0; j < apartments; j++)
        {
            sum3 += house[2, j];
            sum5 += house[4, j];
        }

        Console.WriteLine($"\nКоличество жильцов на 3-м этаже: {sum3}");
        Console.WriteLine($"Количество жильцов на 5-м этаже: {sum5}");

        if (sum3 > sum5)
            Console.WriteLine("На 3-м этаже проживает больше людей");
        else if (sum5 > sum3)
            Console.WriteLine("На 5-м этаже проживает больше людей");
        else
            Console.WriteLine("На 3-м и 5-м этажах проживает одинаковое количество людей");
    }
}