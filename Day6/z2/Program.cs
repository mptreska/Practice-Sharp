using System;

class Doctor
{
    public string Name { get; set; }
    public string Specialty { get; set; }

    public Doctor(string name, string specialty)
    {
        Name = name;
        Specialty = specialty;
    }

    public override string ToString()
    {
        return $"Доктор: {Name}, Специальность: {Specialty}";
    }
}

class MedicalRecord
{
    public string PatientName { get; set; }
    public string Diagnosis { get; set; }

    public MedicalRecord(string patientName, string diagnosis)
    {
        PatientName = patientName;
        Diagnosis = diagnosis;
    }

    public override string ToString()
    {
        return $"Пациент: {PatientName}, Диагноз: {Diagnosis}";
    }
}

class Pharmacy
{
    public string Name { get; set; }

    public Pharmacy(string name) { Name = name; }

    public void SupplyMedicine()
    {
        Console.WriteLine($"Аптека {Name} поставляет лекарства");
    }
}

class Hospital
{
    public string Name { get; set; }
    public Doctor[] Doctors { get; set; }
    private MedicalRecord record;
    private Pharmacy pharmacy;

    public Hospital(string name, Doctor[] doctors, string patientName, string diagnosis, Pharmacy pharmacy)
    {
        Name = name;
        Doctors = doctors;
        record = new MedicalRecord(patientName, diagnosis);
        this.pharmacy = pharmacy;
    }

    public void TreatPatients()
    {
        Console.WriteLine($"\nБольница: {Name}");
        Console.WriteLine("Врачи:");
        for (int i = 0; i < Doctors.Length; i++)
            Console.WriteLine($"  {Doctors[i]}");
        Console.WriteLine($"История болезни: {record}");
        pharmacy.SupplyMedicine();
        Console.WriteLine("Лечение пациентов выполнено");
    }
}

class Program
{
    static void Main()
    {
        Pharmacy pharmacy1 = new Pharmacy("Аптека №1");
        Pharmacy pharmacy2 = new Pharmacy("Аптека №2");

        Doctor d1 = new Doctor("Иванов", "Терапевт");
        Doctor d2 = new Doctor("Петров", "Хирург");
        Doctor d3 = new Doctor("Сидоров", "Кардиолог");

        Hospital[] hospitals = new Hospital[]
        {
            new Hospital("Городская больница №1", new Doctor[] { d1, d2 }, "Алексей", "Грипп", pharmacy1),
            new Hospital("Городская больница №2", new Doctor[] { d2, d3 }, "Мария", "Перелом", pharmacy2)
        };

        for (int i = 0; i < hospitals.Length; i++)
            hospitals[i].TreatPatients();
    }
}