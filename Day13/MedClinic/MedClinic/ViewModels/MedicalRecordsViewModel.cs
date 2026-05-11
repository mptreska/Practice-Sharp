using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MedClinic.Models;
using MedClinic.Services;

namespace MedClinic.ViewModels
{
    public class MedicalRecordsViewModel : INotifyPropertyChanged
    {
        private MedicalRecordService service = new MedicalRecordService();

        public ObservableCollection<Patient> Patients { get; set; }

        private Patient selectedPatient;
        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                SelectedRecord = null;
                filterDate = null;
                OnPropertyChanged(nameof(SelectedPatient));
                OnPropertyChanged(nameof(FilteredRecords));
                if (selectedPatient != null)
                    _ = LoadRecordsAsync(selectedPatient);
            }
        }

        private MedicalRecord selectedRecord;
        public MedicalRecord SelectedRecord
        {
            get => selectedRecord;
            set
            {
                selectedRecord = value;
                OnPropertyChanged(nameof(SelectedRecord));
                // Обновляем состояние кнопок
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set { isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        private string loadingStatus;
        public string LoadingStatus
        {
            get => loadingStatus;
            set { loadingStatus = value; OnPropertyChanged(nameof(LoadingStatus)); }
        }

        private DateTime? filterDate;
        public DateTime? FilterDate
        {
            get => filterDate;
            set
            {
                filterDate = value;
                OnPropertyChanged(nameof(FilterDate));
                OnPropertyChanged(nameof(FilteredRecords));
            }
        }

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
                foreach (var p in Patients)
                    foreach (var r in p.Records)
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
        public ICommand ResetFilterCommand { get; }

        public Action ShowAddPatientWindow { get; set; }
        public Action ShowAddRecordWindow { get; set; }
        public Action ShowEditRecordWindow { get; set; }

        public MedicalRecordsViewModel()
        {
            Patients = new ObservableCollection<Patient>();

            AddPatientCommand = new RelayCommand(
                _ => ShowAddPatientWindow?.Invoke());

            AddRecordCommand = new RelayCommand(
                _ => ShowAddRecordWindow?.Invoke(),
                _ => SelectedPatient != null);

            EditRecordCommand = new RelayCommand(
                _ => ShowEditRecordWindow?.Invoke(),
                _ => SelectedRecord != null);

            // Исправленная команда удаления
            DeleteRecordCommand = new AsyncRelayCommand(
                async _ => await DeleteRecordAsync(),
                _ => SelectedRecord != null && SelectedPatient != null);

            ResetFilterCommand = new RelayCommand(
                _ => FilterDate = null);
        }

        public async Task LoadRecordsAsync(Patient patient)
        {
            IsLoading = true;
            LoadingStatus = "Загрузка истории болезни...";

            patient.Records.Clear();
            var records = await service.LoadRecordsAsync(patient.Id);
            foreach (var r in records)
                patient.Records.Add(r);

            IsLoading = false;
            LoadingStatus = "";

            OnPropertyChanged(nameof(FilteredRecords));
            OnPropertyChanged(nameof(AllRecords));
        }

        public void AddPatient(Patient patient)
        {
            patient.Id = Patients.Count + 1;
            Patients.Add(patient);
            OnPropertyChanged(nameof(AllRecords));
        }

        public async Task AddRecordAsync(MedicalRecord record)
        {
            if (SelectedPatient == null) return;

            IsLoading = true;
            LoadingStatus = "Сохранение записи...";

            await service.SaveRecordAsync(record);
            SelectedPatient.Records.Add(record);

            IsLoading = false;
            LoadingStatus = "";

            OnPropertyChanged(nameof(FilteredRecords));
            OnPropertyChanged(nameof(AllRecords));
        }

        public async Task UpdateRecordAsync(MedicalRecord updated)
        {
            if (SelectedRecord == null) return;

            IsLoading = true;
            LoadingStatus = "Обновление записи...";

            await service.SaveRecordAsync(updated);

            // Обновляем через свойства чтобы сработал INotifyPropertyChanged
            SelectedRecord.Diagnosis = updated.Diagnosis;
            SelectedRecord.Description = updated.Description;
            SelectedRecord.Doctor = updated.Doctor;
            SelectedRecord.Date = updated.Date;

            IsLoading = false;
            LoadingStatus = "";

            OnPropertyChanged(nameof(FilteredRecords));
            OnPropertyChanged(nameof(AllRecords));
        }

        private async Task DeleteRecordAsync()
        {
            if (SelectedRecord == null || SelectedPatient == null) return;

            var result = MessageBox.Show(
                $"Удалить запись '{SelectedRecord.Diagnosis}'?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            IsLoading = true;
            LoadingStatus = "Удаление...";

            await service.DeleteRecordAsync(SelectedRecord);

            // Сохраняем ссылку перед обнулением
            var recordToDelete = SelectedRecord;
            SelectedRecord = null;

            SelectedPatient.Records.Remove(recordToDelete);

            IsLoading = false;
            LoadingStatus = "";

            OnPropertyChanged(nameof(FilteredRecords));
            OnPropertyChanged(nameof(AllRecords));
        }

        public void LoadDefaultPatients()
        {
            var p1 = new Patient { Id = 1, FullName = "Иванов Иван Иванович", Age = 35, Phone = "89001234567" };
            p1.Records.Add(new MedicalRecord { Diagnosis = "Грипп", Description = "Температура 38.5", Doctor = "Петров А.А.", Date = DateTime.Today.AddDays(-10) });
            p1.Records.Add(new MedicalRecord { Diagnosis = "Ангина", Description = "Боль в горле", Doctor = "Сидоров В.В.", Date = DateTime.Today.AddDays(-5) });

            var p2 = new Patient { Id = 2, FullName = "Петрова Мария Сергеевна", Age = 28, Phone = "89009876543" };
            p2.Records.Add(new MedicalRecord { Diagnosis = "Бронхит", Description = "Кашель", Doctor = "Петров А.А.", Date = DateTime.Today.AddDays(-3) });

            Patients.Add(p1);
            Patients.Add(p2);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}