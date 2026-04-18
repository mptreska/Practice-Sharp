using System;
using System.Text.RegularExpressions;

namespace Task9Regex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку для проверки (URL): ");
            string url = Console.ReadLine();

            bool result = IsValidUrl(url);
            Console.WriteLine($"Является ли строка корректным URL? {result}");
        }

        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;

            string pattern = @"^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$";
            
            return Regex.IsMatch(url, pattern);
        }
    }
}