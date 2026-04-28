using System;
using System.Collections;

class Car
{
    public string LicensePlate { get; set; }
    public DateTime EntryTime { get; set; }

    public Car(string licensePlate, DateTime entryTime)
    {
        LicensePlate = licensePlate;
        EntryTime = entryTime;
    }

    public override string ToString()
    {
        return $"Номер: {LicensePlate}, Время въезда: {EntryTime}";
    }
}

class TrafficQueue
{
    private Queue cars = new Queue();

    public void AddCar(Car car)
    {
        cars.Enqueue(car);
        Console.WriteLine($"Машина добавлена: {car}");
    }

    public void RemoveCar()
    {
        if (cars.Count > 0)
        {
            Car car = (Car)cars.Dequeue();
            Console.WriteLine($"Машина выехала: {car}");
        }
        else
        {
            Console.WriteLine("Очередь пуста");
        }
    }

    public void ShowCars()
    {
        if (cars.Count == 0)
        {
            Console.WriteLine("Очередь пуста");
            return;
        }

        Console.WriteLine("Машины в очереди:");
        foreach (Car car in cars)
            Console.WriteLine(car);
    }

    public void FindCar(string licensePlate)
    {
        bool found = false;
        foreach (Car car in cars)
        {
            if (car.LicensePlate == licensePlate)
            {
                Console.WriteLine("Машина найдена: " + car);
                found = true;
                break;
            }
        }

        if (!found)
            Console.WriteLine("Машина не найдена");
    }
}

class Program
{
    static void Main()
    {
        TrafficQueue traffic = new TrafficQueue();

        while (true)
        {
            Console.WriteLine("\n1 - Добавить машину");
            Console.WriteLine("2 - Убрать машину");
            Console.WriteLine("3 - Показать очередь");
            Console.WriteLine("4 - Найти машину");
            Console.WriteLine("0 - Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Введите номер машины: ");
                string plate = Console.ReadLine();
                traffic.AddCar(new Car(plate, DateTime.Now));
            }
            else if (choice == "2")
            {
                traffic.RemoveCar();
            }
            else if (choice == "3")
            {
                traffic.ShowCars();
            }
            else if (choice == "4")
            {
                Console.Write("Введите номер машины для поиска: ");
                string plate = Console.ReadLine();
                traffic.FindCar(plate);
            }
            else if (choice == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Неверный выбор");
            }
        }
    }
}