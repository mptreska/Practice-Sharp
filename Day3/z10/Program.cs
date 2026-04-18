using System;
using System.Text;

namespace Task9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите основную строку: ");
            StringBuilder sb = new StringBuilder(Console.ReadLine());

            Console.Write("Введите подстроку для поиска: ");
            string substr = Console.ReadLine();

            int index = IndexOfInStringBuilder(sb, substr);

            if (index != -1)
                Console.WriteLine($"Подстрока найдена на индексе: {index}");
            else
                Console.WriteLine("Подстрока не найдена.");
        }

        public static int IndexOfInStringBuilder(StringBuilder sb, string value)
        {
            if (sb == null || string.IsNullOrEmpty(value) || value.Length > sb.Length)
                return -1;

            for (int i = 0; i <= sb.Length - value.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < value.Length; j++)
                {
                    if (sb[i + j] != value[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match) return i;
            }

            return -1;
        }
    }
}