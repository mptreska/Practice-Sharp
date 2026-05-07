using System;
using System.Windows;
using MedClinic.Models;

namespace MedClinic
{
    public partial class RecordDialog : Window
    {
        public MedicalRecord Result { get; private set; }

        public RecordDialog()
        {
            InitializeComponent();
            DatePicker.SelectedDate = DateTime.Today;
        }

        public RecordDialog(MedicalRecord record) : this()
        {
            DiagnosisBox.Text = record.Diagnosis;
            DescriptionBox.Text = record.Description;
            DoctorBox.Text = record.Doctor;
            DatePicker.SelectedDate = record.Date;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DiagnosisBox.Text))
            {
                MessageBox.Show("Введите диагноз", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Result = new MedicalRecord
            {
                Diagnosis = DiagnosisBox.Text,
                Description = DescriptionBox.Text,
                Doctor = DoctorBox.Text,
                Date = DatePicker.SelectedDate ?? DateTime.Today
            };
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}