using System;
using System.Windows;
using System.Windows.Controls;
using MedClinic.Models;

namespace MedClinic
{
    public partial class RecordDialog : Window
    {
        public MedicalRecord Result { get; private set; }

        public RecordDialog()
        {
            InitializeComponent();
            RecordDatePicker.SelectedDate = DateTime.Today;
        }

        public RecordDialog(MedicalRecord record) : this()
        {
            DiagnosisBox.Text = record.Diagnosis;
            DescriptionBox.Text = record.Description;
            DoctorBox.Text = record.Doctor;
            RecordDatePicker.SelectedDate = record.Date;

            switch (record.Importance)
            {
                case Importance.Warning:
                    ImportanceBox.SelectedIndex = 1;
                    break;
                case Importance.Critical:
                    ImportanceBox.SelectedIndex = 2;
                    break;
                default:
                    ImportanceBox.SelectedIndex = 0;
                    break;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DiagnosisBox.Text))
            {
                MessageBox.Show("Введите диагноз", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Importance importance = Importance.Normal;
            if (ImportanceBox.SelectedIndex == 1)
                importance = Importance.Warning;
            else if (ImportanceBox.SelectedIndex == 2)
                importance = Importance.Critical;

            Result = new MedicalRecord
            {
                Diagnosis = DiagnosisBox.Text,
                Description = DescriptionBox.Text,
                Doctor = DoctorBox.Text,
                Date = RecordDatePicker.SelectedDate ?? DateTime.Today,
                Importance = importance
            };

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}