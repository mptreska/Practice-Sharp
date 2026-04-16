using System;

namespace PracticeTasks
{
    class CylinderArea
    {
        static void Main()
        {
            Console.WriteLine("Вычисление площади поверхности цилиндра.");
            Console.WriteLine("Введите исходные данные:");

            Console.Write("Радиус основания (см) —> ");
            double radius = double.Parse(Console.ReadLine());

            Console.Write("Высота цилиндра (см) —> ");
            double height = double.Parse(Console.ReadLine());

            double area = 2 * Math.PI * radius * (radius + height);

            Console.WriteLine("Площадь поверхности цилиндра: {0:F2} кв.см.", area);
        }
    }
}