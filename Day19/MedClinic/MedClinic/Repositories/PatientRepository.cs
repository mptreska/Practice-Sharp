using System.Collections.Generic;
using System.Threading.Tasks;
using MedClinic.Models;
using MedClinic.Services;

namespace MedClinic.Repositories
{
    public class PatientRepository : IRepository<Patient>
    {
        private JsonDataService jsonService = new JsonDataService();
        private List<Patient> patients;

        public PatientRepository()
        {
            patients = jsonService.LoadPatients();
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            await Task.Delay(100);
            return patients;
        }

        public async Task AddAsync(Patient item)
        {
            await Task.Delay(50);
            item.Id = patients.Count + 1;
            patients.Add(item);
        }

        public async Task UpdateAsync(Patient item)
        {
            await Task.Delay(50);
            for (int i = 0; i < patients.Count; i++)
                if (patients[i].Id == item.Id)
                    patients[i] = item;
        }

        public async Task DeleteAsync(Patient item)
        {
            await Task.Delay(50);
            patients.Remove(item);
        }

        public async Task SaveAsync()
        {
            await Task.Delay(100);
            jsonService.SavePatients(patients);
        }
    }
}