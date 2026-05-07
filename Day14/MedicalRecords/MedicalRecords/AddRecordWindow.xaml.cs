using System;
using System.Windows;
using MedicalRecords.Models;

namespace MedicalRecords
{
    public partial class AddRecordWindow : Window
    {
        public MedicalRecord NewRecord { get; private set; }

        public AddRecordWindow()
        {
            InitializeComponent();
            RecordDatePicker.SelectedDate = DateTime.Today;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DiagnosisBox.Text))
            {
                MessageBox.Show("Введите диагноз", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewRecord = new MedicalRecord
            {
                Diagnosis = DiagnosisBox.Text,
                Description = DescriptionBox.Text,
                Doctor = DoctorBox.Text,
                Date = RecordDatePicker.SelectedDate ?? DateTime.Today
            };

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}