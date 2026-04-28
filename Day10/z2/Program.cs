using System;
using System.Collections.Generic;
using System.IO;

class Employee
{
    public string Name { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }

    public Employee(string name, string position, decimal salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"{Name,-20} {Position,-20} {Salary,10:F2}";
    }
}

class EmployeeFileWriter
{
    private string filePath;

    public EmployeeFileWriter(string filePath)
    {
        this.filePath = filePath;
    }

    public void WriteEmployeesWithHeader(List<Employee> employees)
    {
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine($"{"Имя",-20} {"Должность",-20} {"Зарплата",10}");
            writer.WriteLine(new string('-', 52));

            foreach (Employee emp in employees)
                writer.WriteLine(emp.ToString());
        }
        Console.WriteLine($"Данные записаны в файл: {filePath}");
    }
}

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee("Иванов Иван", "Программист", 100000),
            new Employee("Петров Пётр", "Менеджер", 80000),
            new Employee("Сидоров Сидор", "Дизайнер", 75000),
            new Employee("Козлов Козёл", "Тестировщик", 90000),
            new Employee("Новиков Никита", "Аналитик", 85000)
        };

        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.data");
        EmployeeFileWriter writer = new EmployeeFileWriter(filePath);
        writer.WriteEmployeesWithHeader(employees);

        Console.WriteLine("\nСодержимое файла:");
        Console.WriteLine(File.ReadAllText(filePath));
    }
}