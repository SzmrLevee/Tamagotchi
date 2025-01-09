    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    namespace TamagotchiLib.Utils
    {
        public class FileManager
        {
            private readonly string filePath;

            public FileManager(string filePath)
            {
                this.filePath = filePath;
            }

            public T LoadData<T>() where T : class
            {
                if (!File.Exists(filePath)) return null;
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(json);
            }

            public void SaveData<T>(T data)
            {
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(data, options);
                File.WriteAllText(filePath, json);
            }
        }
    }
