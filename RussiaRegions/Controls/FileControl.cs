using System;
using System.IO;

namespace RussiaRegions
{
    static class FileControl
    {
        public static bool ReadYN()
        {
            while (true)
            {
                var input = Console.ReadLine().ToLower().Trim();
                if (input.Contains('д'))
                {
                    return true;
                }
                else if (input.Contains('н'))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Введите \"да\" или \"нет\"");
                }
            }
        }

        public static FileTypes ReadFileType()
        {
            Console.WriteLine("Введите тип файла(xml/json): ");
            while (true)
            {
                var input = Console.ReadLine().ToLower().Trim();
                FileTypes? fileType = FileManager.GetFileType(input);
                if (fileType == null)
                {
                    Console.Write("Пожалуйста, введите \"xml\" или \"json\" (без кавычек): ");
                    continue;
                }
                return (FileTypes)fileType;
            }
        }

        public static string ReadPathToSave()
        {
            Console.WriteLine("Текущая директория {0}, используется разделитель {1}",
                Environment.CurrentDirectory, Path.DirectorySeparatorChar);
            while (true)
            {
                Console.Write("Введите путь к файлу: ", Path.DirectorySeparatorChar);
                var fileName = Console.ReadLine();
                if (File.Exists(fileName))
                {
                    Console.Write("Указанный файл существует. Перезаписать? (да/нет)");
                    if (!ReadYN())
                    {
                        continue;
                    }
                }
                return fileName;
            }
        }

        public static string ReadPathToLoad()
        {
            Console.WriteLine("Текущая директория {0}, используется разделитель {1}",
                Environment.CurrentDirectory, Path.DirectorySeparatorChar);
            while (true)
            {
                Console.Write("Введите путь к файлу: ", Path.DirectorySeparatorChar);
                var fileName = Console.ReadLine();
                if (!File.Exists(fileName))
                {
                    Console.WriteLine("Указанный файл не существует.");
                    continue;
                }
                return fileName;
            }
        }
    }
}
