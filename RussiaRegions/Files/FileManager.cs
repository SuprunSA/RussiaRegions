using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Text.Json;

namespace RussiaRegions
{
    static class FileManager
    {
        public static void SaveToJson<ListsDTO>(string fileName, ListsDTO data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(fileName, jsonString);
        }

        public static ListsDTO LoadFromJson<ListsDTO>(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<ListsDTO>(jsonString);
        }

        public static void SaveToXml<ListsDTO>(string fileName, ListsDTO data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ListsDto));
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(fs, data);
            }
        }

        public static ListsDTO LoadFromXml<ListsDTO>(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ListsDTO));
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                return (ListsDTO)serializer.Deserialize(fs);
            }
        }

        public static FileTypes? GetFileType(string extension)
        {
            switch (extension.ToLower().Trim('.'))
            {
                case "xml": return FileTypes.Xml;
                case "json": return FileTypes.Json;
                default: return null;
            }
        }

        public static FileTypes? CheckFileType(string fileName)
        {
            return GetFileType(Path.GetExtension(fileName));
        }
    }
}
