using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MedClinic.Models
{
    public class Patient : INotifyPropertyChanged
    {
        private string fullName;
        private int age;
        private string phone;

        public int Id { get; set; }

        public string FullName
        {
            get => fullName;
            set { fullName = value; OnPropertyChanged(nameof(FullName)); }
        }

        public int Age
        {
            get => age;
            set { age = value; OnPropertyChanged(nameof(Age)); }
        }

        public string Phone
        {
            get => phone;
            set { phone = value; OnPropertyChanged(nameof(Phone)); }
        }

        public ObservableCollection<MedicalRecord> Records { get; set; }

        public Patient()
        {
            Records = new ObservableCollection<MedicalRecord>();
        }

        public override string ToString() => FullName;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}