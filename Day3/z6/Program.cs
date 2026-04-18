using System;
using System.Collections.Generic;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine();

            string result = GetLongestUniqueSubstring(input);
            Console.WriteLine($"Подстрока максимальной длины из уникальных символов: \"{result}\"");
        }

        public static string GetLongestUniqueSubstring(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";

            var charIndexMap = new Dictionary<char, int>();
            int maxLength = 0;
            int start = 0;
            int maxStart = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (charIndexMap.ContainsKey(input[i]))
                {
                    start = Math.Max(start, charIndexMap[input[i]] + 1);
                }
                charIndexMap[input[i]] = i;

                if (i - start + 1 > maxLength)
                {
                    maxLength = i - start + 1;
                    maxStart = start;
                }
            }
            return input.Substring(maxStart, maxLength);
        }
    }
}