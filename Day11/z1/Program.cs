using System;

class ModeController
{
    private static ModeController instance;
    private string currentMode;

    private ModeController()
    {
        currentMode = "обычный";
        Console.WriteLine("ModeController создан. Режим по умолчанию: обычный");
    }

    public static ModeController GetInstance()
    {
        if (instance == null)
            instance = new ModeController();
        return instance;
    }

    public void SetMode(string mode)
    {
        currentMode = mode;
        Console.WriteLine($"Режим изменён на: {currentMode}");
    }

    public string GetMode()
    {
        return currentMode;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Паттерн Singleton: ModeController ===\n");

        ModeController controller1 = ModeController.GetInstance();
        Console.WriteLine($"Текущий режим: {controller1.GetMode()}");

        controller1.SetMode("отладочный");

        ModeController controller2 = ModeController.GetInstance();
        Console.WriteLine($"Режим через второй экземпляр: {controller2.GetMode()}");

        Console.WriteLine($"\nОдин и тот же объект: {ReferenceEquals(controller1, controller2)}");

        while (true)
        {
            Console.Write("\nВведите режим (обычный/отладочный/выход): ");
            string input = Console.ReadLine();

            if (input == "выход")
                break;

            ModeController.GetInstance().SetMode(input);
            Console.WriteLine($"Текущий режим: {ModeController.GetInstance().GetMode()}");
        }
    }
}