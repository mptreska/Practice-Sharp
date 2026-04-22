using System;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

static class StringProcessor
{
    public static string ConcatenateNames(Person[] persons)
    {
        string result = "";
        for (int i = 0; i < persons.Length; i++)
        {
            result += persons[i].Name;
            if (i < persons.Length - 1)
                result += ", ";
        }
        return result;
    }
}

class Program
{
    static void Main()
    {
        Person[] persons = new Person[]
        {
            new Person("Алексей", 25),
            new Person("Мария", 30),
            new Person("Иван", 22),
            new Person("Анна", 28)
        };

        Console.WriteLine("Все имена: " + StringProcessor.ConcatenateNames(persons));
    }
}