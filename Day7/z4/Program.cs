using System;

class WaterLevelEventArgs : EventArgs
{
    public int WaterLevel { get; set; }

    public WaterLevelEventArgs(int waterLevel)
    {
        WaterLevel = waterLevel;
    }
}

class WaterTankSensor
{
    public event EventHandler<WaterLevelEventArgs> WaterLevelChanged;

    private int waterLevel;

    public int WaterLevel
    {
        get { return waterLevel; }
        set
        {
            waterLevel = value;
            WaterLevelChanged?.Invoke(this, new WaterLevelEventArgs(waterLevel));
        }
    }
}

class PumpController
{
    public void OnWaterLevelChanged(object sender, WaterLevelEventArgs e)
    {
        if (e.WaterLevel < 20)
            Console.WriteLine($"[Насос] Уровень воды {e.WaterLevel}% - Насос включён");
        else
            Console.WriteLine($"[Насос] Уровень воды {e.WaterLevel}% - Насос выключен");
    }
}

class WarningSystem
{
    public void OnWaterLevelChanged(object sender, WaterLevelEventArgs e)
    {
        if (e.WaterLevel > 80)
            Console.WriteLine($"[Предупреждение] Уровень воды {e.WaterLevel}% - Опасность переполнения!");
        else
            Console.WriteLine($"[Предупреждение] Уровень воды {e.WaterLevel}% - Норма");
    }
}

class WaterMonitor
{
    private WaterTankSensor sensor;
    private PumpController pump;
    private WarningSystem warning;

    public WaterMonitor(WaterTankSensor sensor)
    {
        this.sensor = sensor;
        pump = new PumpController();
        warning = new WarningSystem();

        sensor.WaterLevelChanged += pump.OnWaterLevelChanged;
        sensor.WaterLevelChanged += warning.OnWaterLevelChanged;
    }
}

class Program
{
    static void Main()
    {
        WaterTankSensor sensor = new WaterTankSensor();
        WaterMonitor monitor = new WaterMonitor(sensor);

        Console.WriteLine("Изменение уровня воды:");

        sensor.WaterLevel = 10;
        Console.WriteLine();

        sensor.WaterLevel = 50;
        Console.WriteLine();

        sensor.WaterLevel = 90;
    }
}