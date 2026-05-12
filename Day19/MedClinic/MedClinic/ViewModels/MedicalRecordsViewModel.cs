using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MedClinic.Models;
using MedClinic.Repositories;
using MedClinic.Services;

namespace MedClinic.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class MedicalRecordsViewModel : BaseViewModel
    {
        private PatientRepository patientRepo = new PatientRepository();
        private RecordRepository recordRepo = new RecordRepository();
        private MedicalRecordService service = new MedicalRecordService();

        public ObservableCollection<Patient> Patients { get; set; }
            = new ObservableCollection<Patient>();

        private Patient selectedPatient;
        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                SelectedRecord = null;
                filterDate = null;
                OnPropertyChanged();
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
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set { isLoading = value; OnPropertyChanged(); }
        }

        private string loadingStatus;
        public string LoadingStatus
        {
            get => loadingStatus;
            set { loadingStatus = value; OnPropertyChanged(); }
        }

        private DateTime? filterDate;
        public DateTime? FilterDate
        {
            get => filterDate;
            set
            {
                filterDate = value;
                OnPropertyChanged();
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
                            r.Doctor,
                            Importance = r.Importance.ToString()
                        });
                return list;
            }
        }

        public ICommand AddPatientCommand { get; }
        public ICommand AddRecordCommand { get; }
        public ICommand EditRecordCommand { get; }
        public ICommand DeleteRecordCommand { get; }
        public ICommand ResetFilterCommand { get; }
        public ICommand LoadPatientsCommand { get; }

        public Action ShowAddPatientWindow { get; set; }
        public Action ShowAddRecordWindow { get; set; }
        public Action ShowEditRecordWindow { get; set; }

        public MedicalRecordsViewModel()
        {
            AddPatientCommand = new RelayCommand(
                _ => ShowAddPatientWindow?.Invoke());

            AddRecordCommand = new RelayCommand(
                _ => ShowAddRecordWindow?.Invoke(),
                _ => SelectedPatient != null);

            EditRecordCommand = new RelayCommand(
                _ => ShowEditRecordWindow?.Invoke(),
                _ => SelectedRecord != null);

            DeleteRecordCommand = new AsyncRelayCommand(
                async _ => await DeleteRecordAsync(),
                _ => SelectedRecord != null && SelectedPatient != null);

            ResetFilterCommand = new RelayCommand(
                _ => FilterDate = null);

            LoadPatientsCommand = new AsyncRelayCommand(
                async _ => await LoadAllPatientsAsync());
        }

        public async Task LoadAllPatientsAsync()
        {
            IsLoading = true;
            LoadingStatus = "Загрузка пациентов...";

            var list = await patientRepo.GetAllAsync();
            Patients.Clear();
            foreach (var p in list)
                Patients.Add(p);

            IsLoading = false;
            LoadingStatus = "";
        }

        public async Task LoadRecordsAsync(Patient patient)
        {
            IsLoading = true;
            LoadingStatus = "Загрузка истории болезни...";

            recordRepo.SetPatient(patient);

            await Task.Delay(500);

            IsLoading = false;
            LoadingStatus = "";

            OnPropertyChanged(nameof(FilteredRecords));
            OnPropertyChanged(nameof(AllRecords));
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await patientRepo.AddAsync(patient);
            await patientRepo.SaveAsync();
            Patients.Add(patient);
            OnPropertyChanged(nameof(AllRecords));
        }

        public void AddPatient(Patient patient)
        {
            _ = AddPatientAsync(patient);
        }

        public async Task AddRecordAsync(MedicalRecord record)
        {
            if (SelectedPatient == null) return;

            IsLoading = true;
            LoadingStatus = "Сохранение записи...";

            await recordRepo.AddAsync(record);
            await recordRepo.SaveAsync();

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

            SelectedRecord.Diagnosis = updated.Diagnosis;
            SelectedRecord.Description = updated.Description;
            SelectedRecord.Doctor = updated.Doctor;
            SelectedRecord.Date = updated.Date;
            SelectedRecord.Importance = updated.Importance;

            await recordRepo.UpdateAsync(SelectedRecord);
            await recordRepo.SaveAsync();

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

            var recordToDelete = SelectedRecord;
            SelectedRecord = null;

            await recordRepo.DeleteAsync(recordToDelete);
            await recordRepo.SaveAsync();

            IsLoading = false;
            LoadingStatus = "";

            OnPropertyChanged(nameof(FilteredRecords));
            OnPropertyChanged(nameof(AllRecords));
        }

        public void LoadDefaultPatients()
        {
            var p1 = new Patient
            {
                Id = 1,
                FullName = "Иванов Иван Иванович",
                Age = 35,
                Phone = "89001234567"
            };
            p1.Records.Add(new MedicalRecord
            {
                Diagnosis = "Грипп",
                Description = "Температура 38.5",
                Doctor = "Петров А.А.",
                Date = DateTime.Today.AddDays(-10),
                Importance = Importance.Warning
            });
            p1.Records.Add(new MedicalRecord
            {
                Diagnosis = "Ангина",
                Description = "Боль в горле",
                Doctor = "Сидоров В.В.",
                Date = DateTime.Today.AddDays(-5),
                Importance = Importance.Normal
            });

            var p2 = new Patient
            {
                Id = 2,
                FullName = "Петрова Мария Сергеевна",
                Age = 28,
                Phone = "89009876543"
            };
            p2.Records.Add(new MedicalRecord
            {
                Diagnosis = "Инфаркт",
                Description = "Срочная госпитализация",
                Doctor = "Петров А.А.",
                Date = DateTime.Today.AddDays(-3),
                Importance = Importance.Critical
            });

            Patients.Add(p1);
            Patients.Add(p2);
        }
    }
}