using System.Windows;
using MedicalRecords.Models;

namespace MedicalRecords
{
    public partial class AddPatientWindow : Window
    {
        public Patient NewPatient { get; private set; }

        public AddPatientWindow() { InitializeComponent(); }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameBox.Text))
            {
                MessageBox.Show("Введите ФИО", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewPatient = new Patient
            {
                FullName = FullNameBox.Text,
                Age = int.TryParse(AgeBox.Text, out int age) ? age : 0,
                Phone = PhoneBox.Text
            };

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}