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

class EmployeeFileReader
{
    private string filePath;

    public EmployeeFileReader(string filePath)
    {
        this.filePath = filePath;
    }

    public List<Employee> ReadEmployees()
    {
        List<Employee> employees = new List<Employee>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не найден");
            return employees;
        }

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 2; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string name = line.Substring(0, 20).Trim();
            string position = line.Substring(20, 20).Trim();
            decimal salary = decimal.Parse(line.Substring(40).Trim());

            employees.Add(new Employee(name, position, salary));
        }

        Console.WriteLine($"Прочитано сотрудников: {employees.Count}");
        return employees;
    }
}

class EmployeeProcessor
{
    private List<Employee> employees;

    public EmployeeProcessor(List<Employee> employees)
    {
        this.employees = employees;
    }

    public List<Employee> FilterBySalary(decimal minSalary)
    {
        List<Employee> result = new List<Employee>();
        foreach (Employee emp in employees)
            if (emp.Salary >= minSalary)
                result.Add(emp);
        return result;
    }

    public List<Employee> SortBySalary()
    {
        List<Employee> sorted = new List<Employee>(employees);
        sorted.Sort((a, b) => b.Salary.CompareTo(a.Salary));
        return sorted;
    }
}

class Program
{
    static void Main()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.data");
        EmployeeFileReader reader = new EmployeeFileReader(filePath);
        List<Employee> employees = reader.ReadEmployees();

        Console.WriteLine("\nВсе сотрудники:");
        Console.WriteLine($"{"Имя",-20} {"Должность",-20} {"Зарплата",10}");
        Console.WriteLine(new string('-', 52));
        foreach (Employee emp in employees)
            Console.WriteLine(emp);

        EmployeeProcessor processor = new EmployeeProcessor(employees);

        Console.Write("\nВведите минимальную зарплату для фильтрации: ");
        decimal minSalary = decimal.Parse(Console.ReadLine());

        List<Employee> filtered = processor.FilterBySalary(minSalary);
        Console.WriteLine($"\nСотрудники с зарплатой >= {minSalary}:");
        Console.WriteLine($"{"Имя",-20} {"Должность",-20} {"Зарплата",10}");
        Console.WriteLine(new string('-', 52));
        foreach (Employee emp in filtered)
            Console.WriteLine(emp);

        Console.WriteLine("\nСортировка по убыванию зарплаты:");
        Console.WriteLine($"{"Имя",-20} {"Должность",-20} {"Зарплата",10}");
        Console.WriteLine(new string('-', 52));
        foreach (Employee emp in processor.SortBySalary())
            Console.WriteLine(emp);
    }
}