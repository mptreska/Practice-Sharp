using System;

class ObjectAccessException : Exception
{
    public ObjectAccessException() : base("Ошибка доступа к объекту") { }

    public ObjectAccessException(string message) : base(message) { }

    public ObjectAccessException(string message, Exception innerException) : base(message, innerException) { }
}

class ObjectManager
{
    public void AccessObject(object obj)
    {
        if (obj == null)
            throw new NullReferenceException("Объект равен null");

        Console.WriteLine($"Доступ к объекту: {obj}");
    }
}

class ObjectHandler
{
    private ObjectManager manager = new ObjectManager();

    public void HandleObject(object obj)
    {
        try
        {
            manager.AccessObject(obj);
        }
        catch (NullReferenceException ex)
        {
            throw new ObjectAccessException("Невозможно получить доступ к объекту", ex);
        }
    }
}

class Logger
{
    public static void Log(Exception ex)
    {
        Console.WriteLine($"\n=== Лог ошибки ===");
        Console.WriteLine($"Тип: {ex.GetType().Name}");
        Console.WriteLine($"Сообщение: {ex.Message}");
        Console.WriteLine($"Стек вызовов:\n{ex.StackTrace}");

        if (ex.InnerException != null)
        {
            Console.WriteLine($"\n=== Внутреннее исключение ===");
            Console.WriteLine($"Тип: {ex.InnerException.GetType().Name}");
            Console.WriteLine($"Сообщение: {ex.InnerException.Message}");
            Console.WriteLine($"Стек вызовов:\n{ex.InnerException.StackTrace}");
        }
    }
}

class Program
{
    static void Main()
    {
        ObjectHandler handler = new ObjectHandler();

        Console.WriteLine("Введите данные объекта (или оставьте пустым для null):");
        string input = Console.ReadLine();

        object obj = string.IsNullOrEmpty(input) ? null : input;

        try
        {
            handler.HandleObject(obj);
        }
        catch (ObjectAccessException ex)
        {
            Logger.Log(ex);
        }
    }
}