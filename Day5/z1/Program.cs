using System;

static class StringHelper
{
    public static string ReverseString(string s)
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
        string s = Console.ReadLine();
        Console.WriteLine("Перевернутая строка: " + StringHelper.ReverseString(s));
    }
}