using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using MedClinic.Models;
using MedClinic.Services;
using MedClinic.ViewModels;

namespace MedClinic
{
    public partial class MainWindow : Window
    {
        private MedicalRecordsViewModel viewModel;
        private AuthService authService;
        private JsonDataService jsonService;
        private ChatService chatService;
        private NotificationService notificationService;
        private User currentUser;

        public MainWindow(AuthService authService)
        {
            InitializeComponent();

            this.authService = authService;
            this.currentUser = authService.CurrentUser;

            jsonService = new JsonDataService();
            chatService = new ChatService();
            notificationService = new NotificationService();

            viewModel = new MedicalRecordsViewModel();
            DataContext = viewModel;

            viewModel.ShowAddPatientWindow = OpenAddPatient;
            viewModel.ShowAddRecordWindow = OpenAddRecord;
            viewModel.ShowEditRecordWindow = OpenEditRecord;

            Title = $"Медицинские записи — {currentUser.FullName}";
            UserInfoText.Text = $"👤 {currentUser.FullName} | {currentUser.Role}";

            notificationService.NotificationReceived += OnNotification;
            notificationService.StartListening();

            LoadPatients();
        }

        private void LoadPatients()
        {
            var patients = jsonService.LoadPatients();
            viewModel.Patients.Clear();

            if (patients.Count == 0)
            {
                viewModel.LoadDefaultPatients();
            }
            else
            {
                foreach (var p in patients)
                    viewModel.Patients.Add(p);
            }
        }

        private void SavePatients()
        {
            jsonService.SavePatients(
                new List<Patient>(viewModel.Patients));
        }

        private void OnNotification(string message)
        {
            Dispatcher.Invoke(() =>
            {
                NotificationText.Text = message;
                NotificationPanel.Visibility = Visibility.Visible;

                var timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(5)
                };
                timer.Tick += (s, e) =>
                {
                    NotificationPanel.Visibility = Visibility.Collapsed;
                    timer.Stop();
                };
                timer.Start();
            });
        }

        private void OpenAddPatient()
        {
            if (currentUser.Role != UserRole.Doctor)
            {
                MessageBox.Show("Только врачи могут добавлять пациентов",
                    "Доступ запрещён", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var win = new PatientDialog { Owner = this };
            if (win.ShowDialog() == true)
            {
                viewModel.AddPatient(win.Result);
                SavePatients();
                notificationService.SendNotification(
                    $"🏥 Новый пациент: {win.Result.FullName}");
            }
        }

        private async void OpenAddRecord()
        {
            if (currentUser.Role != UserRole.Doctor)
            {
                MessageBox.Show("Только врачи могут добавлять записи",
                    "Доступ запрещён", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var win = new RecordDialog { Owner = this };
            if (win.ShowDialog() == true)
            {
                await viewModel.AddRecordAsync(win.Result);
                SavePatients();
                notificationService.SendNotification(
                    $"📋 Новая запись: {win.Result.Diagnosis}");
            }
        }

        private async void OpenEditRecord()
        {
            if (currentUser.Role != UserRole.Doctor)
            {
                MessageBox.Show("Только врачи могут редактировать записи",
                    "Доступ запрещён", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (viewModel.SelectedRecord == null) return;

            var win = new RecordDialog(viewModel.SelectedRecord) { Owner = this };
            if (win.ShowDialog() == true)
            {
                await viewModel.UpdateRecordAsync(win.Result);
                SavePatients();
            }
        }

        private void OpenChat_Click(object sender, RoutedEventArgs e)
        {
            var chatWindow = new ChatWindow(currentUser, chatService)
            {
                Owner = this
            };
            chatWindow.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void About_Click(object sender, RoutedEventArgs e) =>
            MessageBox.Show(
                "Программа учёта медицинских записей\n" +
                "Версия 3.0 | MVVM + Auth + Chat + JSON",
                "О программе",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

        protected override void OnClosed(EventArgs e)
        {
            chatService.Stop();
            notificationService.Stop();
            base.OnClosed(e);
        }
    }
}