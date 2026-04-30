using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MedicalRecords.Models;

namespace MedicalRecords
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Patient> patients = new ObservableCollection<Patient>();

        public MainWindow()
        {
            InitializeComponent();
            LoadSampleData();
            PatientListBox.ItemsSource = patients;
        }

        private void LoadSampleData()
        {
            Patient p1 = new Patient { Id = 1, FullName = "Иванов Иван Иванович", Age = 35, Phone = "89001234567" };
            p1.Records.Add(new MedicalRecord { Diagnosis = "Грипп", Description = "Температура 38.5", Doctor = "Петров А.А.", Date = DateTime.Today.AddDays(-10) });
            p1.Records.Add(new MedicalRecord { Diagnosis = "Ангина", Description = "Боль в горле", Doctor = "Сидоров В.В.", Date = DateTime.Today.AddDays(-5) });

            Patient p2 = new Patient { Id = 2, FullName = "Петрова Мария Сергеевна", Age = 28, Phone = "89009876543" };
            p2.Records.Add(new MedicalRecord { Diagnosis = "Бронхит", Description = "Кашель, затруднённое дыхание", Doctor = "Петров А.А.", Date = DateTime.Today.AddDays(-3) });

            patients.Add(p1);
            patients.Add(p2);
            RefreshAllRecords();
        }

        private void PatientListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientListBox.SelectedItem is Patient patient)
            {
                PatientNameText.Text = patient.FullName;
                PatientAgeText.Text = $"Возраст: {patient.Age} лет";
                PatientPhoneText.Text = $"Телефон: {patient.Phone}";
                RecordsDataGrid.ItemsSource = patient.Records;
            }
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            AddPatientWindow win = new AddPatientWindow();
            win.Owner = this;
            if (win.ShowDialog() == true)
            {
                win.NewPatient.Id = patients.Count + 1;
                patients.Add(win.NewPatient);
                RefreshAllRecords();
            }
        }

        private void DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (PatientListBox.SelectedItem is Patient patient)
            {
                if (MessageBox.Show($"Удалить {patient.FullName}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    patients.Remove(patient);
                    PatientNameText.Text = "";
                    PatientAgeText.Text = "";
                    PatientPhoneText.Text = "";
                    RecordsDataGrid.ItemsSource = null;
                    RefreshAllRecords();
                }
            }
            else
                MessageBox.Show("Выберите пациента", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            if (PatientListBox.SelectedItem is Patient patient)
            {
                AddRecordWindow win = new AddRecordWindow();
                win.Owner = this;
                if (win.ShowDialog() == true)
                {
                    patient.Records.Add(win.NewRecord);
                    RefreshAllRecords();
                }
            }
            else
                MessageBox.Show("Выберите пациента", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void RefreshAllRecords()
        {
            var all = new List<object>();
            foreach (Patient p in patients)
                foreach (MedicalRecord r in p.Records)
                    all.Add(new { PatientName = p.FullName, r.Date, r.Diagnosis, r.Description, r.Doctor });
            AllRecordsDataGrid.ItemsSource = all;
        }
    }
}