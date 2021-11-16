using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Text.Json;

namespace RussiaRegions
{
    static class FileManager
    {
        public static void SaveToJson(string fileName, ListsDTO data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(fileName, jsonString);
        }

        public static ListsDTO LoadFromJson(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<ListsDTO>(jsonString);
        }

        public static void SaveToXml(string fileName, ListsDTO data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ListsDTO));
            using var fs = new FileStream(fileName, FileMode.Create);
            serializer.Serialize(fs, data);
        }

        public static ListsDTO LoadFromXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ListsDTO));
            using var fs = new FileStream(fileName, FileMode.Open);
            return (ListsDTO)serializer.Deserialize(fs);
        }

        public static FileTypes? GetFileType(string extension)
        {
            return extension.ToLower().Trim('.') switch
            {
                "xml" => FileTypes.Xml,
                "josn" => FileTypes.Json,
                _ => null
            };
        }

        public static FileTypes? CheckFileType(string fileName)
        {
            return GetFileType(Path.GetExtension(fileName));
        }
    }
}
