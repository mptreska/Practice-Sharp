using System;
using System.Collections.Generic;
using System.Threading;

interface IPlayerObserver
{
    void Update(string playerName, int position, int lap);
}

class RaceGame
{
    private List<IPlayerObserver> observers = new List<IPlayerObserver>();
    private string playerName;
    private int position;
    private int lap;
    private int totalLaps;

    public RaceGame(string playerName, int totalLaps)
    {
        this.playerName = playerName;
        this.totalLaps = totalLaps;
        position = 0;
        lap = 1;
    }

    public void Subscribe(IPlayerObserver observer)
    {
        observers.Add(observer);
        Console.WriteLine($"Подписчик {observer.GetType().Name} добавлен");
    }

    public void Unsubscribe(IPlayerObserver observer)
    {
        observers.Remove(observer);
        Console.WriteLine($"Подписчик {observer.GetType().Name} удалён");
    }

    private void NotifyObservers()
    {
        foreach (IPlayerObserver observer in observers)
            observer.Update(playerName, position, lap);
    }

    public void StartRace()
    {
        Console.WriteLine($"\n=== Гонка началась! Игрок: {playerName}, Кругов: {totalLaps} ===\n");

        while (lap <= totalLaps)
        {
            position += new Random().Next(10, 30);
            NotifyObservers();
            Thread.Sleep(1000);

            if (position >= 100)
            {
                position = 0;
                lap++;
                if (lap <= totalLaps)
                    Console.WriteLine($"\n--- Круг {lap - 1} завершён! Начинается круг {lap} ---\n");
            }
        }

        Console.WriteLine($"\n=== {playerName} финишировал! ===");
        NotifyObservers();
    }
}

class Spectator : IPlayerObserver
{
    private string name;

    public Spectator(string name)
    {
        this.name = name;
    }

    public void Update(string playerName, int position, int lap)
    {
        Console.WriteLine($"[Зритель {name}] {playerName} на позиции {position}% круга {lap}");
    }
}

class RaceCommentator : IPlayerObserver
{
    private string name;

    public RaceCommentator(string name)
    {
        this.name = name;
    }

    public void Update(string playerName, int position, int lap)
    {
        string comment;

        if (position < 30)
            comment = "только начинает круг";
        else if (position < 60)
            comment = "на середине трассы";
        else if (position < 90)
            comment = "приближается к финишу круга";
        else
            comment = "завершает круг!";

        Console.WriteLine($"[Комментатор {name}] {playerName} {comment}! Круг {lap}, позиция {position}%");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Паттерн Наблюдатель: Гоночная игра ===\n");

        Console.Write("Введите имя игрока: ");
        string playerName = Console.ReadLine();

        Console.Write("Введите количество кругов: ");
        int totalLaps = int.Parse(Console.ReadLine());

        RaceGame game = new RaceGame(playerName, totalLaps);

        Spectator spectator1 = new Spectator("Алексей");
        Spectator spectator2 = new Spectator("Мария");
        RaceCommentator commentator1 = new RaceCommentator("Иван");
        RaceCommentator commentator2 = new RaceCommentator("Пётр");

        game.Subscribe(spectator1);
        game.Subscribe(spectator2);
        game.Subscribe(commentator1);
        game.Subscribe(commentator2);

        Console.WriteLine("\nОтписываем одного зрителя:");
        game.Unsubscribe(spectator2);

        game.StartRace();
    }
}