using System;

class Report
{
    public string Title { get; set; }
    public string Header { get; set; }
    public string Body { get; set; }
    public string Footer { get; set; }
    public string Format { get; set; }

    public override string ToString()
    {
        return $"\n=== Отчёт ({Format}) ===" +
               $"\nЗаголовок: {Title}" +
               $"\nШапка: {Header}" +
               $"\nСодержимое: {Body}" +
               $"\nПодвал: {Footer}";
    }
}

interface IReportBuilder
{
    void SetTitle(string title);
    void BuildHeader();
    void BuildBody();
    void BuildFooter();
    Report GetReport();
}

class PDFReportBuilder : IReportBuilder
{
    private Report report = new Report();

    public void SetTitle(string title) { report.Title = title; report.Format = "PDF"; }
    public void BuildHeader() { report.Header = "[PDF] Шапка отчёта"; }
    public void BuildBody() { report.Body = "[PDF] Основное содержимое отчёта в формате PDF"; }
    public void BuildFooter() { report.Footer = "[PDF] Подвал - Страница 1 из 1"; }
    public Report GetReport() { return report; }
}

class WordReportBuilder : IReportBuilder
{
    private Report report = new Report();

    public void SetTitle(string title) { report.Title = title; report.Format = "Word"; }
    public void BuildHeader() { report.Header = "[Word] Шапка документа Word"; }
    public void BuildBody() { report.Body = "[Word] Основное содержимое документа Word"; }
    public void BuildFooter() { report.Footer = "[Word] Подвал - Автор: Капыцкий Н.Д."; }
    public Report GetReport() { return report; }
}

class ExcelReportBuilder : IReportBuilder
{
    private Report report = new Report();

    public void SetTitle(string title) { report.Title = title; report.Format = "Excel"; }
    public void BuildHeader() { report.Header = "[Excel] Заголовки таблицы: Имя | Должность | Зарплата"; }
    public void BuildBody() { report.Body = "[Excel] Данные таблицы: Строки с данными"; }
    public void BuildFooter() { report.Footer = "[Excel] Итого: сумма по столбцам"; }
    public Report GetReport() { return report; }
}

class ReportDirector
{
    private IReportBuilder builder;

    public ReportDirector(IReportBuilder builder)
    {
        this.builder = builder;
    }

    public void SetBuilder(IReportBuilder builder)
    {
        this.builder = builder;
    }

    public Report BuildFullReport(string title)
    {
        builder.SetTitle(title);
        builder.BuildHeader();
        builder.BuildBody();
        builder.BuildFooter();
        return builder.GetReport();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Паттерн Строитель: Report ===");

        ReportDirector director = new ReportDirector(new PDFReportBuilder());

        Console.Write("Введите название отчёта: ");
        string title = Console.ReadLine();

        Console.WriteLine("\nВыберите формат (1-PDF, 2-Word, 3-Excel): ");
        string choice = Console.ReadLine();

        IReportBuilder builder;
        switch (choice)
        {
            case "1": builder = new PDFReportBuilder(); break;
            case "2": builder = new WordReportBuilder(); break;
            case "3": builder = new ExcelReportBuilder(); break;
            default:
                Console.WriteLine("Неверный выбор, используется PDF");
                builder = new PDFReportBuilder();
                break;
        }

        director.SetBuilder(builder);
        Report report = director.BuildFullReport(title);
        Console.WriteLine(report);

        Console.WriteLine("\n--- Создание всех форматов ---");
        director.SetBuilder(new PDFReportBuilder());
        Console.WriteLine(director.BuildFullReport("Ежемесячный отчёт"));

        director.SetBuilder(new WordReportBuilder());
        Console.WriteLine(director.BuildFullReport("Ежемесячный отчёт"));

        director.SetBuilder(new ExcelReportBuilder());
        Console.WriteLine(director.BuildFullReport("Ежемесячный отчёт"));
    }
}