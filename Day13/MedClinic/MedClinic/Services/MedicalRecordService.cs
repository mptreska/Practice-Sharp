using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MedClinic.Models;

namespace MedClinic.Services
{
    public class MedicalRecordService
    {
        // Имитация загрузки данных из базы данных
        public async Task<ObservableCollection<MedicalRecord>> LoadRecordsAsync(int patientId)
        {
            // Имитируем задержку загрузки (как будто идёт запрос к БД)
            await Task.Delay(2000);

            var records = new ObservableCollection<MedicalRecord>();

            if (patientId == 1)
            {
                records.Add(new MedicalRecord
                {
                    Diagnosis = "Грипп",
                    Description = "Температура 38.5, кашель",
                    Doctor = "Петров А.А.",
                    Date = DateTime.Today.AddDays(-10)
                });
                records.Add(new MedicalRecord
                {
                    Diagnosis = "Ангина",
                    Description = "Боль в горле",
                    Doctor = "Сидоров В.В.",
                    Date = DateTime.Today.AddDays(-5)
                });
            }
            else if (patientId == 2)
            {
                records.Add(new MedicalRecord
                {
                    Diagnosis = "Бронхит",
                    Description = "Кашель, затруднённое дыхание",
                    Doctor = "Петров А.А.",
                    Date = DateTime.Today.AddDays(-3)
                });
            }

            return records;
        }

        public async Task SaveRecordAsync(MedicalRecord record)
        {
            // Имитация сохранения в БД
            await Task.Delay(500);
            Console.WriteLine($"Запись сохранена: {record.Diagnosis}");
        }

        public async Task DeleteRecordAsync(MedicalRecord record)
        {
            // Имитация удаления из БД
            await Task.Delay(300);
            Console.WriteLine($"Запись удалена: {record.Diagnosis}");
        }
    }
}