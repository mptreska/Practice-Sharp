using System.Windows;
using System.Windows.Input;
using MedClinic.Models;
using MedClinic.Services;

namespace MedClinic
{
    public partial class ChatWindow : Window
    {
        private ChatService chatService;
        private User currentUser;

        public ChatWindow(User currentUser, ChatService chatService)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            this.chatService = chatService;

            TitleText.Text = $"💬 Чат | {currentUser.FullName}";

            chatService.MessageReceived += OnMessageReceived;
            chatService.StartListening();

            AddMessage("✅ Сервер чата запущен");
            AddMessage($"Вы вошли как: {currentUser.FullName} ({currentUser.Role})");
        }

        private void OnMessageReceived(string message)
        {
            Dispatcher.Invoke(() => AddMessage(message));
        }

        private void AddMessage(string message)
        {
            MessagesBox.Items.Add(message);
            if (MessagesBox.Items.Count > 0)
                MessagesBox.ScrollIntoView(
                    MessagesBox.Items[MessagesBox.Items.Count - 1]);
        }

        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            string text = InputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(text)) return;

            InputBox.Text = "";
            await chatService.SendMessageAsync(currentUser.FullName, text);
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Send_Click(sender, null);
        }

        protected override void OnClosed(System.EventArgs e)
        {
            chatService.MessageReceived -= OnMessageReceived;
            base.OnClosed(e);
        }
    }
}