using System.Windows;
using MedClinic.Services;

namespace MedClinic
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var authService = new AuthService();
            var loginWindow = new LoginWindow(authService);

            if (loginWindow.ShowDialog() == true)
            {
                var mainWindow = new MainWindow(authService);
                mainWindow.Show();
            }
            else
            {
                Shutdown();
            }
        }
    }
}