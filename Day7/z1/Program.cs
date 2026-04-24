using System;

delegate void LightControl(string location);

class RoomLight
{
    public void TurnOn(string location)
    {
        Console.WriteLine($"Свет в комнате {location} включён");
    }

    public void TurnOff(string location)
    {
        Console.WriteLine($"Свет в комнате {location} выключен");
    }
}

class StreetLight
{
    public void TurnOn(string location)
    {
        Console.WriteLine($"Уличный свет на {location} включён");
    }

    public void TurnOff(string location)
    {
        Console.WriteLine($"Уличный свет на {location} выключен");
    }
}

class Program
{
    static void Main()
    {
        RoomLight roomLight = new RoomLight();
        StreetLight streetLight = new StreetLight();

        LightControl turnOn = roomLight.TurnOn;
        turnOn += streetLight.TurnOn;

        LightControl turnOff = roomLight.TurnOff;
        turnOff += streetLight.TurnOff;

        Console.WriteLine("Включаем свет:");
        turnOn("спальня");

        Console.WriteLine("\nВыключаем свет:");
        turnOff("главная улица");
    }
}