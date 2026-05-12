using System.Windows;
using MedClinic.Models;
using MedClinic.Services;

namespace MedClinic
{
    public partial class LoginWindow : Window
    {
        private AuthService authService;
        public User LoggedInUser { get; private set; }

        public LoginWindow(AuthService authService)
        {
            InitializeComponent();
            this.authService = authService;
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AnimationHelper.FadeIn(MainGrid, 0.6);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                LoginErrorText.Text = "Введите логин и пароль";
                return;
            }

            if (authService.Login(username, password))
            {
                LoggedInUser = authService.CurrentUser;
                DialogResult = true;
            }
            else
            {
                LoginErrorText.Text = "Неверный логин или пароль";
                AnimationHelper.Pulse(LoginErrorText);
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string fullName = RegFullNameBox.Text.Trim();
            string username = RegLoginBox.Text.Trim();
            string password = RegPasswordBox.Password;
            UserRole role = RoleComboBox.SelectedIndex == 0
                              ? UserRole.Doctor : UserRole.Patient;

            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                RegErrorText.Foreground = System.Windows.Media.Brushes.Red;
                RegErrorText.Text = "Заполните все поля";
                AnimationHelper.Pulse(RegErrorText);
                return;
            }

            if (authService.Register(username, password, fullName, role))
            {
                RegErrorText.Foreground = System.Windows.Media.Brushes.Green;
                RegErrorText.Text = "✅ Зарегистрировано! Войдите в систему";
            }
            else
            {
                RegErrorText.Foreground = System.Windows.Media.Brushes.Red;
                RegErrorText.Text = "Такой логин уже занят";
                AnimationHelper.Pulse(RegErrorText);
            }
        }
    }
}