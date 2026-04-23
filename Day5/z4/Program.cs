using System;

static class StringExtensions
{
    public static string ReverseText(this string s)
    {
        char[] chars = s.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string text = Console.ReadLine();
        Console.WriteLine(text.ReverseText());
    }
}