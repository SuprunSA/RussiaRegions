using System;
using System.Collections.Generic;
using System.Linq;

namespace RussiaRegions
{
    class FileSelector
    {
        public static void SaveToFile<T>(string title, IEnumerable<T> data)
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
                        FileManager.SaveToXml(fileName, data.ToArray());
                        break;
                }
                Console.WriteLine("Файл успешно сохранен.");
            }
            catch (Exception e)
            {
                Console.WriteLine("При сохранении файла произошла ошибка: " + e.ToString());
            }
        }

        public static IEnumerable<T> LoadFromFile<T>(string title)
        {
            Console.Clear();
            Console.WriteLine("{0}: загрузка из файла", title);
            Console.WriteLine("Все существующие файлы будут удалены! Продолжить?");
            if (!FileControl.ReadYN())
            {
                return null;
            }
            string fileName = FileControl.ReadPathToLoad();
            FileTypes? fileTypes = FileManager.CheckFileType(fileName);
            IEnumerable<T> data = null;
            try
            {
                switch (fileTypes)
                {
                    case FileTypes.Xml: data = FileManager.LoadFromXml<T>(fileName); break;
                    case FileTypes.Json: data = FileManager.LoadFromJson<T>(fileName); break;
                    default: throw new InvalidOperationException("Формат файла не распознан. Используйте XML или JSON.");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Ошибка при загрузке из файла: {0}", e.ToString());
            }
            return data;
        }
    }
}
