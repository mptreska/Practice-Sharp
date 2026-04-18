using System;
using System.Collections.Generic;

namespace Task8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine();

            Console.Write("Введите длину одной части (целое число): ");
            if (int.TryParse(Console.ReadLine(), out int chunkSize) && chunkSize > 0)
            {
                List<string> result = SplitByLength(input, chunkSize);
                Console.WriteLine("Результат разбиения:");
                foreach (var chunk in result)
                {
                    Console.WriteLine($"\"{chunk}\"");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: введено некорректное число. Длина должна быть больше 0.");
            }
        }

        public static List<string> SplitByLength(string input, int length)
        {
            List<string> result = new List<string>();

            if (string.IsNullOrEmpty(input)) return result;

            for (int i = 0; i < input.Length; i += length)
            {
                int takeLength = Math.Min(length, input.Length - i);
                result.Add(input.Substring(i, takeLength));
            }

            return result;
        }
    }
}