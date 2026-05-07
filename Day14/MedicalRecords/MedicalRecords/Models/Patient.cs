using System.Collections.ObjectModel;

namespace MedicalRecords.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public ObservableCollection<MedicalRecord> Records { get; set; }

        public Patient()
        {
            Records = new ObservableCollection<MedicalRecord>();
        }

        public override string ToString() => FullName;
    }
}