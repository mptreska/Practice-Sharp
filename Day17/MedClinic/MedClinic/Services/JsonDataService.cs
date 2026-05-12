using System;
using System.Collections.Generic;
using System.IO;
using MedClinic.Models;
using Newtonsoft.Json;

namespace MedClinic.Services
{
    public class JsonDataService
    {
        private string patientsFile;
        private string usersFile;

        public JsonDataService()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            patientsFile = Path.Combine(folder, "medical.json");
            usersFile = Path.Combine(folder, "users.json");
        }

        public void SavePatients(List<Patient> patients)
        {
            string json = JsonConvert.SerializeObject(patients, Formatting.Indented);
            File.WriteAllText(patientsFile, json);
        }

        public List<Patient> LoadPatients()
        {
            if (!File.Exists(patientsFile))
                return new List<Patient>();

            try
            {
                string json = File.ReadAllText(patientsFile);
                return JsonConvert.DeserializeObject<List<Patient>>(json)
                       ?? new List<Patient>();
            }
            catch
            {
                return new List<Patient>();
            }
        }

        public void SaveUsers(List<User> users)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(usersFile, json);
        }

        public List<User> LoadUsers()
        {
            if (!File.Exists(usersFile))
            {
                var defaults = GetDefaultUsers();
                SaveUsers(defaults);
                return defaults;
            }

            try
            {
                string json = File.ReadAllText(usersFile);
                return JsonConvert.DeserializeObject<List<User>>(json)
                       ?? GetDefaultUsers();
            }
            catch
            {
                return GetDefaultUsers();
            }
        }

        private List<User> GetDefaultUsers()
        {
            return new List<User>
            {
                new User { Id = 1, Username = "doctor1",  Password = "1234",
                           FullName = "Петров Алексей Иванович",   Role = UserRole.Doctor  },
                new User { Id = 2, Username = "patient1", Password = "1234",
                           FullName = "Иванов Иван Иванович",      Role = UserRole.Patient },
                new User { Id = 3, Username = "doctor2",  Password = "1234",
                           FullName = "Сидоров Виктор Петрович",   Role = UserRole.Doctor  }
            };
        }
    }
}