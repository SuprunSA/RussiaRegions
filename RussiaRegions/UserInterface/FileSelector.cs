using System;
using System.Collections.Generic;
using System.Linq;

namespace RussiaRegions
{
    class FileSelector
    {
        public static void SaveToFile(string title, ListsDTO data)
        {
            Console.Clear();
            Console.WriteLine("{0}: сохранение в файл", title);
            string fileName = FileControl.ReadPathToSave();
            FileTypes fileTypes = FileControl.ReadFileType();
            try
            {
                switch (fileTypes)
                {
                    case FileTypes.Json: 
                        FileManager.SaveToJson(fileName, data);
                        break;
                    case FileTypes.Xml:
                        FileManager.SaveToXml(fileName, data);
                        break;
                }
                Console.WriteLine("Файл успешно сохранен.");
            }
            catch (Exception e)
            {
                Console.WriteLine("При сохранении файла произошла ошибка: " + e.ToString());
            }
        }

        public static ListsDTO LoadFromFile(string title)
        {
            Console.Clear();
            Console.WriteLine("{0}: загрузка из файла", title);
            Console.WriteLine("Все существующие файлы будут удалены! Продолжить?");
            if (!FileControl.ReadYN())
            {
                return default;
            }
            string fileName = FileControl.ReadPathToLoad();
            FileTypes? fileTypes = FileManager.CheckFileType(fileName);
            ListsDTO data = default;
            try
            {
                data = fileTypes switch
                {
                    FileTypes.Xml => FileManager.LoadFromXml(fileName),
                    FileTypes.Json => FileManager.LoadFromJson(fileName),
                    _ => throw new InvalidOperationException("Формат файла не распознан. Используйте XML или JSON."),
                };
            }
            catch(Exception e)
            {
                Console.WriteLine("Ошибка при загрузке из файла: {0}", e.ToString());
            }
            return data;
        }
    }
}
