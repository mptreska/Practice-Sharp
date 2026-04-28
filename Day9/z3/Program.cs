using System;

interface IEvent<T>
{
    void Trigger(T data);
}

class SimpleEvent<T> : IEvent<T>
{
    public event Action<T> OnTriggered;

    public void Trigger(T data)
    {
        Console.WriteLine($"Событие вызвано с данными: {data}");
        OnTriggered?.Invoke(data);
    }
}

class EventManager<T>
{
    private IEvent<T> eventHandler;

    public EventManager(IEvent<T> eventHandler)
    {
        this.eventHandler = eventHandler;
    }

    public void RaiseEvent(T data)
    {
        eventHandler.Trigger(data);
    }
}

class Program
{
    static void Main()
    {
        SimpleEvent<string> simpleEvent = new SimpleEvent<string>();
        simpleEvent.OnTriggered += data => Console.WriteLine($"Обработчик получил: {data}");

        EventManager<string> manager = new EventManager<string>(simpleEvent);

        while (true)
        {
            Console.Write("\nВведите сообщение для события (или exit): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
                break;

            manager.RaiseEvent(input);
        }
    }
}