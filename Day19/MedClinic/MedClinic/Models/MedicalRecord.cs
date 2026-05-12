using System;
using System.ComponentModel;

namespace MedClinic.Models
{
    public enum Importance
    {
        Normal,
        Warning,
        Critical
    }

    public class MedicalRecord : INotifyPropertyChanged
    {
        private string diagnosis;
        private string description;
        private string doctor;
        private DateTime date;
        private Importance importance;

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

        public Importance Importance
        {
            get => importance;
            set { importance = value; OnPropertyChanged(nameof(Importance)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}