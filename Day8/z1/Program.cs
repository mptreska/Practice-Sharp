using System;

class TooManyLoginAttemptsException : Exception
{
    public TooManyLoginAttemptsException() : base("Превышено количество попыток входа") { }

    public TooManyLoginAttemptsException(string message) : base(message) { }

    public TooManyLoginAttemptsException(string message, Exception innerException) : base(message, innerException) { }
}

class LoginManager
{
    private int maxAttempts = 3;

    public void AttemptLogin(string login, string password)
    {
        string correctLogin = "admin";
        string correctPassword = "1234";
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Console.Write("Введите логин: ");
            login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            password = Console.ReadLine();

            attempts++;

            if (login == correctLogin && password == correctPassword)
            {
                Console.WriteLine("Вход выполнен успешно!");
                return;
            }

            Console.WriteLine($"Неверный логин или пароль. Попытка {attempts} из {maxAttempts}");
        }

        throw new TooManyLoginAttemptsException($"Превышено количество попыток входа. Максимум: {maxAttempts}");
    }
}

class Program
{
    static void Main()
    {
        LoginManager manager = new LoginManager();

        try
        {
            manager.AttemptLogin("", "");
        }
        catch (TooManyLoginAttemptsException ex)
        {
            Console.WriteLine($"\nОшибка: {ex.Message}");
        }
    }
}