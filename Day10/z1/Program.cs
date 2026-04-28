using System;
using System.IO;

class FileManager
{
    public void CreateFile(string path, string text)
    {
        File.WriteAllText(path, text);
        Console.WriteLine($"Файл создан: {path}");
    }

    public void ReadFile(string path)
    {
        if (File.Exists(path))
            Console.WriteLine($"Содержимое файла:\n{File.ReadAllText(path)}");
        else
            Console.WriteLine("Файл не найден");
    }

    public void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Console.WriteLine($"Файл удалён: {path}");
        }
        else
            Console.WriteLine("Ошибка: файл не существует, удаление невозможно");
    }

    public void CopyFile(string source, string dest)
    {
        if (File.Exists(source))
        {
            File.Copy(source, dest, true);
            Console.WriteLine($"Файл скопирован: {dest}");
            Console.WriteLine($"Копия существует: {File.Exists(dest)}");
        }
        else
            Console.WriteLine("Исходный файл не найден");
    }

    public void MoveFile(string source, string dest)
    {
        if (File.Exists(source))
        {
            string dir = Path.GetDirectoryName(dest);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.Move(source, dest, true);
            Console.WriteLine($"Файл перемещён в: {dest}");
        }
        else
            Console.WriteLine("Исходный файл не найден");
    }

    public void RenameFile(string source, string newName)
    {
        if (File.Exists(source))
        {
            string dir = Path.GetDirectoryName(source);
            string dest = Path.Combine(dir, newName);
            File.Move(source, dest, true);
            Console.WriteLine($"Файл переименован в: {newName}");
        }
        else
            Console.WriteLine("Файл не найден");
    }

    public void CompareFilesBySize(string path1, string path2)
    {
        if (File.Exists(path1) && File.Exists(path2))
        {
            long size1 = new FileInfo(path1).Length;
            long size2 = new FileInfo(path2).Length;
            Console.WriteLine($"Размер файла 1: {size1} байт");
            Console.WriteLine($"Размер файла 2: {size2} байт");

            if (size1 > size2)
                Console.WriteLine($"{Path.GetFileName(path1)} больше");
            else if (size2 > size1)
                Console.WriteLine($"{Path.GetFileName(path2)} больше");
            else
                Console.WriteLine("Файлы одинакового размера");
        }
        else
            Console.WriteLine("Один из файлов не найден");
    }

    public void DeleteFilesByExtension(string folderPath, string extension)
    {
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, $"*.{extension}");
            foreach (string file in files)
            {
                File.Delete(file);
                Console.WriteLine($"Удалён файл: {file}");
            }
            Console.WriteLine($"Удалено файлов: {files.Length}");
        }
        else
            Console.WriteLine("Папка не найдена");
    }

    public void ListFiles(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);
            Console.WriteLine($"Файлы в папке {folderPath}:");
            foreach (string file in files)
                Console.WriteLine($"  - {Path.GetFileName(file)}");
        }
        else
            Console.WriteLine("Папка не найдена");
    }

    public void SetReadOnly(string path)
    {
        if (File.Exists(path))
        {
            File.SetAttributes(path, FileAttributes.ReadOnly);
            Console.WriteLine($"Файл {path} сделан только для чтения");

            try
            {
                File.WriteAllText(path, "Попытка записи");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Ошибка записи: {ex.Message}");
            }
            finally
            {
                File.SetAttributes(path, FileAttributes.Normal);
            }
        }
        else
            Console.WriteLine("Файл не найден");
    }

    public void CheckFilePermissions(string path)
    {
        if (File.Exists(path))
        {
            FileAttributes attrs = File.GetAttributes(path);
            Console.WriteLine($"Чтение: всегда доступно");
            Console.WriteLine($"Запись: {(attrs.HasFlag(FileAttributes.ReadOnly) ? "запрещена" : "разрешена")}");
            Console.WriteLine($"Только для чтения: {attrs.HasFlag(FileAttributes.ReadOnly)}");
        }
        else
            Console.WriteLine("Файл не найден");
    }
}

class FileInfoProvider
{
    public void GetFileInfo(string path)
    {
        if (File.Exists(path))
        {
            FileInfo info = new FileInfo(path);
            Console.WriteLine($"Имя: {info.Name}");
            Console.WriteLine($"Размер: {info.Length} байт");
            Console.WriteLine($"Дата создания: {info.CreationTime}");
            Console.WriteLine($"Дата изменения: {info.LastWriteTime}");
            Console.WriteLine($"Расширение: {info.Extension}");
        }
        else
            Console.WriteLine("Файл не найден");
    }
}

class Program
{
    static void Main()
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        string filePath = Path.Combine(baseDir, "kapytsky.nd");
        string copyPath = Path.Combine(baseDir, "kapytsky_copy.nd");
        string newDir = Path.Combine(baseDir, "NewFolder");
        string movedPath = Path.Combine(newDir, "kapytsky.nd");
        string renamedPath = Path.Combine(baseDir, "kapytsky.io");

        FileManager fm = new FileManager();
        FileInfoProvider fip = new FileInfoProvider();

        Console.WriteLine("=== 1. Создание и чтение файла ===");
        fm.CreateFile(filePath, "Капыцкий Никита Дмитриевич\nДолжность: Программист\nЗарплата: 100000");
        fm.ReadFile(filePath);

        Console.WriteLine("\n=== 2. Информация о файле ===");
        fip.GetFileInfo(filePath);

        Console.WriteLine("\n=== 3. Копирование файла ===");
        fm.CopyFile(filePath, copyPath);

        Console.WriteLine("\n=== 4. Сравнение файлов по размеру ===");
        fm.CompareFilesBySize(filePath, copyPath);

        Console.WriteLine("\n=== 5. Список файлов в папке ===");
        fm.ListFiles(baseDir);

        Console.WriteLine("\n=== 6. Перемещение файла ===");
        fm.MoveFile(copyPath, movedPath);

        Console.WriteLine("\n=== 7. Переименование файла ===");
        fm.RenameFile(filePath, "kapytsky.io");

        Console.WriteLine("\n=== 8. Запрет записи в файл ===");
        fm.SetReadOnly(renamedPath);

        Console.WriteLine("\n=== 9. Проверка прав доступа ===");
        fm.CheckFilePermissions(renamedPath);

        Console.WriteLine("\n=== 10. Удаление файлов по расширению .nd ===");
        fm.DeleteFilesByExtension(baseDir, "nd");

        Console.WriteLine("\n=== 11. Удаление несуществующего файла ===");
        fm.DeleteFile("несуществующий_файл.txt");

        Console.WriteLine("\n=== 12. Удаление оставшихся файлов ===");
        fm.DeleteFile(renamedPath);
        fm.DeleteFile(movedPath);
    }
}