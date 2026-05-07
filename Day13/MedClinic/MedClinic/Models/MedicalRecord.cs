using System;
using System.ComponentModel;

namespace MedClinic.Models
{
    public class MedicalRecord : INotifyPropertyChanged
    {
        private string diagnosis;
        private string description;
        private string doctor;
        private DateTime date;

        public string Diagnosis
        {
            get => diagnosis;
            set { diagnosis = value; OnPropertyChanged(nameof(Diagnosis)); }
        }

        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }

        public string Doctor
        {
            get => doctor;
            set { doctor = value; OnPropertyChanged(nameof(Doctor)); }
        }

        public DateTime Date
        {
            get => date;
            set { date = value; OnPropertyChanged(nameof(Date)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}