using System;

delegate void MessageDelegate(string sender, string message);

class ChatApplication
{
    public event MessageDelegate MessageReceived;

    public void SendMessage(string sender, string message)
    {
        Console.WriteLine($"\nНовое сообщение от {sender}: {message}");
        MessageReceived?.Invoke(sender, message);
    }
}

class DesktopNotifier
{
    public void ShowNotification(string sender, string message)
    {
        Console.WriteLine($"[Уведомление на рабочем столе] От: {sender} - {message}");
    }
}

class SoundAlert
{
    public void PlaySound(string sender, string message)
    {
        Console.WriteLine($"[Звуковой сигнал] Получено сообщение от {sender}");
    }
}

class Program
{
    static void Main()
    {
        ChatApplication chat = new ChatApplication();
        DesktopNotifier desktop = new DesktopNotifier();
        SoundAlert sound = new SoundAlert();

        chat.MessageReceived += desktop.ShowNotification;
        chat.MessageReceived += sound.PlaySound;

        chat.SendMessage("Алексей", "Привет!");
        chat.SendMessage("Мария", "Как дела?");
    }
}