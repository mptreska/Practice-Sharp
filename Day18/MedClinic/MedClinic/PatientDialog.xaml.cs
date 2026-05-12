using System.Windows;
using MedClinic.Models;

namespace MedClinic
{
    public partial class PatientDialog : Window
    {
        public Patient Result { get; private set; }

        public PatientDialog() { InitializeComponent(); }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите ФИО", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Result = new Patient
            {
                FullName = NameBox.Text,
                Age = int.TryParse(AgeBox.Text, out int a) ? a : 0,
                Phone = PhoneBox.Text
            };
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}