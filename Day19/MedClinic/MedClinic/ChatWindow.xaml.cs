using System.Collections.ObjectModel;
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

        private static ObservableCollection<string> messages
            = new ObservableCollection<string>();

        public ChatWindow(User currentUser, ChatService chatService)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            this.chatService = chatService;

            TitleText.Text = $"💬 Чат | {currentUser.FullName}";

            MessagesBox.ItemsSource = messages;

            chatService.MessageReceived += OnMessageReceived;

            if (messages.Count == 0)
            {
                messages.Add("✅ Сервер чата запущен");
                messages.Add($"Вы вошли как: {currentUser.FullName} ({currentUser.Role})");
            }

            ScrollToBottom();
        }

        private void OnMessageReceived(string message)
        {
            Dispatcher.Invoke(() =>
            {
                messages.Add(message);
                ScrollToBottom();
            });
        }

        private void ScrollToBottom()
        {
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

        protected override void OnClosing(
            System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}