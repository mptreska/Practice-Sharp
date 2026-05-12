using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedClinic.Models;

namespace MedClinic.Repositories
{
    public class RecordRepository : IRepository<MedicalRecord>
    {
        private List<MedicalRecord> records = new List<MedicalRecord>();
        private Patient currentPatient;

        public void SetPatient(Patient patient)
        {
            currentPatient = patient;
            records = patient?.Records?.ToList()
                      ?? new List<MedicalRecord>();
        }

        public async Task<List<MedicalRecord>> GetAllAsync()
        {
            await Task.Delay(100);
            return records;
        }

        public async Task AddAsync(MedicalRecord item)
        {
            await Task.Delay(50);
            records.Add(item);
            currentPatient?.Records?.Add(item);
        }

        public async Task UpdateAsync(MedicalRecord item)
        {
            await Task.Delay(50);
            var existing = records.FirstOrDefault(r => r == item);
            if (existing != null)
            {
                existing.Diagnosis = item.Diagnosis;
                existing.Description = item.Description;
                existing.Doctor = item.Doctor;
                existing.Date = item.Date;
                existing.Importance = item.Importance;
            }
        }

        public async Task DeleteAsync(MedicalRecord item)
        {
            await Task.Delay(50);
            records.Remove(item);
            currentPatient?.Records?.Remove(item);
        }

        public async Task SaveAsync()
        {
            await Task.Delay(100);
        }
    }
}