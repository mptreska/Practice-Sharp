using System;

class MyObservableList<T>
{
    private T[] items = new T[10];
    private int count = 0;

    public event Action<T> ItemAdded;
    public event Action<T> ItemRemoved;

    public int Count
    {
        get { return count; }
    }

    public void Add(T item)
    {
        if (count == items.Length)
        {
            T[] newItems = new T[items.Length * 2];
            for (int i = 0; i < items.Length; i++)
                newItems[i] = items[i];
            items = newItems;
        }

        items[count] = item;
        count++;
        ItemAdded?.Invoke(item);
    }

    public bool Remove(T item)
    {
        int index = IndexOf(item);
        if (index == -1)
            return false;

        for (int i = index; i < count - 1; i++)
            items[i] = items[i + 1];

        count--;
        items[count] = default(T);
        ItemRemoved?.Invoke(item);
        return true;
    }

    public int IndexOf(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (Equals(items[i], item))
                return i;
        }
        return -1;
    }

    public void ShowAll()
    {
        for (int i = 0; i < count; i++)
            Console.WriteLine(items[i]);
    }
}

class ObservableListManager<T>
{
    private MyObservableList<T> list;

    public ObservableListManager(MyObservableList<T> list)
    {
        this.list = list;
        list.ItemAdded += OnItemAdded;
        list.ItemRemoved += OnItemRemoved;
    }

    private void OnItemAdded(T item)
    {
        Console.WriteLine($"Добавлен элемент: {item}");
    }

    private void OnItemRemoved(T item)
    {
        Console.WriteLine($"Удалён элемент: {item}");
    }
}

class Program
{
    static void Main()
    {
        MyObservableList<string> list = new MyObservableList<string>();
        ObservableListManager<string> manager = new ObservableListManager<string>(list);

        while (true)
        {
            Console.WriteLine("\n1 - Добавить");
            Console.WriteLine("2 - Удалить");
            Console.WriteLine("3 - Показать");
            Console.WriteLine("0 - Выход");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Введите элемент: ");
                string item = Console.ReadLine();
                list.Add(item);
            }
            else if (choice == "2")
            {
                Console.Write("Введите элемент для удаления: ");
                string item = Console.ReadLine();
                if (!list.Remove(item))
                    Console.WriteLine("Элемент не найден");
            }
            else if (choice == "3")
            {
                Console.WriteLine("Список элементов:");
                list.ShowAll();
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