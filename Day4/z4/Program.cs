using System;

partial class Tour
{
    public string Destination { get; set; }
    public int Duration { get; set; }
    public double Price { get; set; }
    public string Hotel { get; set; }

    public Tour(string destination, int duration, double price, string hotel)
    {
        Destination = destination;
        Duration = duration;
        Price = price;
        Hotel = hotel;
    }

    public override string ToString()
    {
        return $"Направление: {Destination}, Длительность: {Duration} дн., Цена: {Price} руб., Отель: {Hotel}";
    }
}

partial class Tour
{
    public Tour GetLongestTour(Tour[] tours)
    {
        Tour longest = tours[0];
        for (int i = 1; i < tours.Length; i++)
            if (tours[i].Duration > longest.Duration)
                longest = tours[i];
        return longest;
    }

    public Tour[] GetToursByDestination(Tour[] tours, string destination)
    {
        int count = 0;
        for (int i = 0; i < tours.Length; i++)
            if (tours[i].Destination == destination)
                count++;

        Tour[] result = new Tour[count];
        int index = 0;
        for (int i = 0; i < tours.Length; i++)
            if (tours[i].Destination == destination)
                result[index++] = tours[i];

        return result;
    }
}

class TravelAgency
{
    public Tour[] Tours { get; set; }

    public TravelAgency(Tour[] tours)
    {
        Tours = tours;
    }
}

class Program
{
    static void Main()
    {
        TravelAgency agency = new TravelAgency(new Tour[]
        {
            new Tour("Турция", 14, 85000, "Hilton"),
            new Tour("Египет", 10, 65000, "Marriott"),
            new Tour("Турция", 7, 55000, "Sheraton"),
            new Tour("Таиланд", 21, 120000, "Hyatt"),
            new Tour("Египет", 14, 75000, "Radisson")
        });

        Console.WriteLine("Все туры:");
        for (int i = 0; i < agency.Tours.Length; i++)
            Console.WriteLine(agency.Tours[i]);

        Tour helper = new Tour("", 0, 0, "");

        Console.WriteLine("\nСамый длительный тур:");
        Console.WriteLine(helper.GetLongestTour(agency.Tours));

        Console.Write("\nВведите направление для поиска: ");
        string destination = Console.ReadLine();

        Tour[] found = helper.GetToursByDestination(agency.Tours, destination);
        Console.WriteLine($"\nТуры в {destination}:");
        if (found.Length == 0)
            Console.WriteLine("Туры не найдены");
        else
            for (int i = 0; i < found.Length; i++)
                Console.WriteLine(found[i]);
    }
}