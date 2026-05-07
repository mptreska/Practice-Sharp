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

        // Коллекция пациентов
        public ObservableCollection<Patient> Patients { get; set; }

        private Patient selectedPatient;
        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
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
            set { selectedRecord = value; OnPropertyChanged(nameof(SelectedRecord)); }
        }

        // Индикатор загрузки
        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set { isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        // Статус загрузки
        private string loadingStatus;
        public string LoadingStatus
        {
            get => loadingStatus;
            set { loadingStatus = value; OnPropertyChanged(nameof(loadingStatus)); }
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

        // OneWay - фильтрованные записи
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

        // Команды
        public ICommand AddPatientCommand { get; }
        public ICommand AddRecordCommand { get; }
        public ICommand EditRecordCommand { get; }
        public ICommand DeleteRecordCommand { get; }
        public ICommand ResetFilterCommand { get; }

        // Действия для открытия окон (задаются из View)
        public Action ShowAddPatientWindow { get; set; }
        public Action ShowAddRecordWindow { get; set; }
        public Action ShowEditRecordWindow { get; set; }

        public MedicalRecordsViewModel()
        {
            Patients = new ObservableCollection<Patient>();

            AddPatientCommand = new RelayCommand(_ => ShowAddPatientWindow?.Invoke());
            AddRecordCommand = new RelayCommand(_ => ShowAddRecordWindow?.Invoke(), _ => SelectedPatient != null);
            EditRecordCommand = new RelayCommand(_ => ShowEditRecordWindow?.Invoke(), _ => SelectedRecord != null);
            DeleteRecordCommand = new RelayCommand(async _ => await DeleteRecordAsync(), _ => SelectedRecord != null);
            ResetFilterCommand = new RelayCommand(_ => FilterDate = null);

            LoadSamplePatients();
        }

        private void LoadSamplePatients()
        {
            Patients.Add(new Patient { Id = 1, FullName = "Иванов Иван Иванович", Age = 35, Phone = "89001234567" });
            Patients.Add(new Patient { Id = 2, FullName = "Петрова Мария Сергеевна", Age = 28, Phone = "89009876543" });
            Patients.Add(new Patient { Id = 3, FullName = "Сидоров Алексей Владимирович", Age = 45, Phone = "89007654321" });
        }

        // Асинхронная загрузка записей пациента
        public async Task LoadRecordsAsync(Patient patient)
        {
            IsLoading = true;
            LoadingStatus = "Загрузка истории болезни...";

            patient.Records.Clear();

            var records = await service.LoadRecordsAsync(patient.Id);

            foreach (var record in records)
                patient.Records.Add(record);

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

            if (MessageBox.Show($"Удалить запись '{SelectedRecord.Diagnosis}'?",
                "Подтверждение", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                IsLoading = true;
                LoadingStatus = "Удаление записи...";

                await service.DeleteRecordAsync(SelectedRecord);
                SelectedPatient.Records.Remove(SelectedRecord);
                SelectedRecord = null;

                IsLoading = false;
                LoadingStatus = "";

                OnPropertyChanged(nameof(FilteredRecords));
                OnPropertyChanged(nameof(AllRecords));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}