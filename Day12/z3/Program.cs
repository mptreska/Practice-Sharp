using System;
using System.Collections.Generic;

interface ICommand
{
    void Execute();
    void Undo();
}

class ChatService
{
    private List<string> messages = new List<string>();

    public void SendMessage(string recipient, string message)
    {
        messages.Add($"[{recipient}]: {message}");
        Console.WriteLine($"Сообщение отправлено [{recipient}]: {message}");
    }

    public void DeleteLastMessage(string recipient)
    {
        if (messages.Count > 0)
        {
            string last = messages[messages.Count - 1];
            messages.RemoveAt(messages.Count - 1);
            Console.WriteLine($"Сообщение удалено: {last}");
        }
        else
            Console.WriteLine("Нет сообщений для удаления");
    }

    public void ShowHistory()
    {
        Console.WriteLine("\n--- История сообщений ---");
        if (messages.Count == 0)
            Console.WriteLine("История пуста");
        else
            foreach (string msg in messages)
                Console.WriteLine(msg);
    }
}

class SendMessageCommand : ICommand
{
    private ChatService service;
    private string recipient;
    private string message;

    public SendMessageCommand(ChatService service, string recipient, string message)
    {
        this.service = service;
        this.recipient = recipient;
        this.message = message;
    }

    public void Execute() => service.SendMessage(recipient, message);
    public void Undo() => service.DeleteLastMessage(recipient);
}

class ChatClient
{
    private List<ICommand> history = new List<ICommand>();

    public void SendCommand(ICommand command)
    {
        command.Execute();
        history.Add(command);
    }

    public void UndoLast()
    {
        if (history.Count > 0)
        {
            ICommand last = history[history.Count - 1];
            last.Undo();
            history.RemoveAt(history.Count - 1);
        }
        else
            Console.WriteLine("Нет команд для отмены");
    }
}

class Program
{
    static void Main()
    {
        ChatService service = new ChatService();
        ChatClient client = new ChatClient();

        while (true)
        {
            Console.WriteLine("\n1 - Отправить сообщение");
            Console.WriteLine("2 - Отменить последнее");
            Console.WriteLine("3 - История сообщений");
            Console.WriteLine("4 - Выход");
            Console.Write("Выбор: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Получатель: ");
                string recipient = Console.ReadLine();

                Console.Write("Сообщение: ");
                string message = Console.ReadLine();

                client.SendCommand(new SendMessageCommand(service, recipient, message));
            }
            else if (choice == "2")
                client.UndoLast();
            else if (choice == "3")
                service.ShowHistory();
            else if (choice == "4")
                break;
        }
    }
}