using System;

class LowBatteryException : Exception
{
    public int BatteryLevel { get; set; }

    public LowBatteryException() : base("Критически низкий уровень заряда батареи") { }

    public LowBatteryException(string message) : base(message) { }

    public LowBatteryException(string message, int level) : base(message)
    {
        BatteryLevel = level;
    }

    public LowBatteryException(string message, Exception innerException) : base(message, innerException) { }
}

class BatteryManager
{
    public void CheckBatteryLevel(int level)
    {
        if (level < 0 || level > 100)
            throw new ArgumentException("Уровень заряда должен быть от 0 до 100");

        if (level < 5)
            throw new LowBatteryException($"Уровень заряда {level}% - критически низкий! Подключите зарядку", level);

        if (level < 20)
            Console.WriteLine($"Уровень заряда {level}% - низкий, рекомендуется зарядить");
        else
            Console.WriteLine($"Уровень заряда {level}% - норма");
    }
}

class Program
{
    static void Main()
    {
        BatteryManager battery = new BatteryManager();

        while (true)
        {
            Console.Write("\nВведите уровень заряда (0-100) или -1 для выхода: ");
            string input = Console.ReadLine();

            if (input == "-1")
                break;

            try
            {
                int level = int.Parse(input);
                battery.CheckBatteryLevel(level);
            }
            catch (LowBatteryException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine($"Текущий заряд: {ex.BatteryLevel}%");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка ввода: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введите числовое значение");
            }
        }
    }
}