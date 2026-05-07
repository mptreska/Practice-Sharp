using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MedClinic.Models;

namespace MedClinic
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Patient> Patients { get; set; }

        private Patient selectedPatient;
        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                filterDate = null;
                FilterDatePicker.SelectedDate = null;
                OnPropertyChanged(nameof(SelectedPatient));
                OnPropertyChanged(nameof(SelectedPatientRecords));
                OnPropertyChanged(nameof(FilteredRecords));
            }
        }

        private MedicalRecord selectedRecord;
        public MedicalRecord SelectedRecord
        {
            get => selectedRecord;
            set { selectedRecord = value; OnPropertyChanged(nameof(SelectedRecord)); }
        }

        private DateTime? filterDate;

        // OneWay - только чтение списка приёмов
        public ObservableCollection<MedicalRecord> SelectedPatientRecords =>
            SelectedPatient?.Records ?? new ObservableCollection<MedicalRecord>();

        // Фильтрованные записи
        public IEnumerable<MedicalRecord> FilteredRecords
        {
            get
            {
                if (SelectedPatient == null)
                    return new List<MedicalRecord>();

                if (filterDate == null)
                    return SelectedPatient.Records;

                return SelectedPatient.Records
                    .Where(r => r.Date.Date >= filterDate.Value.Date);
            }
        }

        public List<object> AllRecords
        {
            get
            {
                var list = new List<object>();
                foreach (Patient p in Patients)
                    foreach (MedicalRecord r in p.Records)
                        list.Add(new
                        {
                            PatientName = p.FullName,
                            r.Date,
                            r.Diagnosis,
                            r.Description,
                            r.Doctor
                        });
                return list;
            }
        }

        public ICommand AddPatientCommand { get; }
        public ICommand AddRecordCommand { get; }
        public ICommand EditRecordCommand { get; }
        public ICommand DeleteRecordCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            Patients = new ObservableCollection<Patient>();
            DataContext = this;

            AddPatientCommand = new RelayCommand(_ => OpenAddPatient());
            AddRecordCommand = new RelayCommand(_ => OpenAddRecord(), _ => SelectedPatient != null);
            EditRecordCommand = new RelayCommand(_ => OpenEditRecord(), _ => SelectedRecord != null);
            DeleteRecordCommand = new RelayCommand(_ => DeleteRecord(), _ => SelectedRecord != null);

            LoadSampleData();
        }

        private void LoadSampleData()
        {
            Patient p1 = new Patient { Id = 1, FullName = "Иванов Иван Иванович", Age = 35, Phone = "89001234567" };
            p1.Records.Add(new MedicalRecord { Diagnosis = "Грипп", Description = "Температура 38.5", Doctor = "Петров А.А.", Date = DateTime.Today.AddDays(-10) });
            p1.Records.Add(new MedicalRecord { Diagnosis = "Ангина", Description = "Боль в горле", Doctor = "Сидоров В.В.", Date = DateTime.Today.AddDays(-5) });

            Patient p2 = new Patient { Id = 2, FullName = "Петрова Мария Сергеевна", Age = 28, Phone = "89009876543" };
            p2.Records.Add(new MedicalRecord { Diagnosis = "Бронхит", Description = "Кашель", Doctor = "Петров А.А.", Date = DateTime.Today.AddDays(-3) });

            Patients.Add(p1);
            Patients.Add(p2);
        }

        private void OpenAddPatient()
        {
            var win = new PatientDialog { Owner = this };
            if (win.ShowDialog() == true)
            {
                win.Result.Id = Patients.Count + 1;
                Patients.Add(win.Result);
                OnPropertyChanged(nameof(AllRecords));
            }
        }

        private void OpenAddRecord()
        {
            var win = new RecordDialog { Owner = this };
            if (win.ShowDialog() == true)
            {
                SelectedPatient.Records.Add(win.Result);
                OnPropertyChanged(nameof(FilteredRecords));
                OnPropertyChanged(nameof(AllRecords));
            }
        }

        private void OpenEditRecord()
        {
            if (SelectedRecord == null) return;
            var win = new RecordDialog(SelectedRecord) { Owner = this };
            if (win.ShowDialog() == true)
            {
                SelectedRecord.Diagnosis = win.Result.Diagnosis;
                SelectedRecord.Description = win.Result.Description;
                SelectedRecord.Doctor = win.Result.Doctor;
                SelectedRecord.Date = win.Result.Date;
                OnPropertyChanged(nameof(FilteredRecords));
                OnPropertyChanged(nameof(AllRecords));
            }
        }

        private void DeleteRecord()
        {
            if (SelectedRecord == null) return;
            if (MessageBox.Show($"Удалить запись '{SelectedRecord.Diagnosis}'?",
                "Подтверждение", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SelectedPatient.Records.Remove(SelectedRecord);
                SelectedRecord = null;
                OnPropertyChanged(nameof(FilteredRecords));
                OnPropertyChanged(nameof(AllRecords));
            }
        }

        private void FilterDatePicker_Changed(object sender, SelectionChangedEventArgs e)
        {
            filterDate = FilterDatePicker.SelectedDate;
            OnPropertyChanged(nameof(FilteredRecords));
        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e)
        {
            filterDate = null;
            FilterDatePicker.SelectedDate = null;
            OnPropertyChanged(nameof(FilteredRecords));
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void About_Click(object sender, RoutedEventArgs e) =>
            MessageBox.Show("Программа учёта медицинских записей\nВерсия 1.0",
                "О программе", MessageBoxButton.OK, MessageBoxImage.Information);

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}