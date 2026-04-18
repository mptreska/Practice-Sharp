using System;

namespace Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите исходную строку: ");
            string source = Console.ReadLine();

            Console.Write("Введите слово для поиска: ");
            string word = Console.ReadLine();

            bool result = ContainsWord(source, word);
            Console.WriteLine($"Содержит ли строка слово \"{word}\"? {result}");
        }

        public static bool ContainsWord(string source, string word)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(word))
                return false;

            return source.Contains(word);
        }
    }
}