using System;

namespace MedicalRecords.Models
{
    public class MedicalRecord
    {
        public string Diagnosis { get; set; }
        public string Description { get; set; }
        public string Doctor { get; set; }
        public DateTime Date { get; set; }
    }
}