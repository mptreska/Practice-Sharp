using System;
using System.IO;

class FileWatcher
{
    private FileSystemWatcher watcher;
    private string watchFolder;

    public FileWatcher(string folderPath)
    {
        watchFolder = folderPath;

        if (!Directory.Exists(watchFolder))
        {
            Directory.CreateDirectory(watchFolder);
            Console.WriteLine($"Папка создана: {watchFolder}");
        }

        watcher = new FileSystemWatcher(watchFolder);
        watcher.EnableRaisingEvents = true;
        watcher.NotifyFilter = NotifyFilters.FileName;

        watcher.Created += OnFileCreated;
        watcher.Deleted += OnFileDeleted;
        watcher.Changed += OnFileChanged;
        watcher.Renamed += OnFileRenamed;

        Console.WriteLine($"Слежение за папкой: {watchFolder}");
    }

    private void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"\n[Создан] {e.Name}");
        HandleDuplicate(e.FullPath, e.Name);
    }

    private void OnFileDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"\n[Удалён] {e.Name}");
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"\n[Изменён] {e.Name}");
    }

    private void OnFileRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"\n[Переименован] {e.OldName} -> {e.Name}");
    }

    private void HandleDuplicate(string fullPath, string fileName)
    {
        string nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
        string ext = Path.GetExtension(fileName);
        string copyPath = Path.Combine(watchFolder, $"{nameWithoutExt}_copy{ext}");

        if (File.Exists(copyPath))
        {
            Console.WriteLine($"[Дубликат] Файл {fileName} уже существует как копия");
            return;
        }

        if (nameWithoutExt.EndsWith("_copy"))
        {
            Console.WriteLine($"[Дубликат] Файл {fileName} является копией");
            return;
        }

        try
        {
            System.Threading.Thread.Sleep(100);
            if (File.Exists(fullPath))
            {
                File.Copy(fullPath, copyPath);
                Console.WriteLine($"[Дубликат] Создана копия: {Path.GetFileName(copyPath)}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Ошибка] {ex.Message}");
        }
    }

    public void Stop()
    {
        watcher.EnableRaisingEvents = false;
        watcher.Dispose();
        Console.WriteLine("Слежение остановлено");
    }
}

class Program
{
    static void Main()
    {
        string watchFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WatchFolder");
        FileWatcher fileWatcher = new FileWatcher(watchFolder);

        Console.WriteLine("Программа запущена. Нажмите Enter для выхода...");
        Console.ReadLine();

        fileWatcher.Stop();
    }
}