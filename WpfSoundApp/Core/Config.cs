using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSoundApp.Core
{
    public class ConfigData
    {
        public string Version { get; set; }
        /// <summary>
        /// tKay = App. 
        /// tValue = version.
        /// </summary>
        public Dictionary<int, string> Apps { get; set; }
        public ConfigData(string v, Dictionary<int, string> a)
        {
            Version = v;
            Apps = a;
        }
        public ConfigData()
        {
            Version = "";
            Apps = new Dictionary<int, string>();
        }

        // Додайте інші властивості, які ви хочете прочитати з файлу
    }
    static class Config
    {
        public const string Version = "DevBuild-20230601-1";

        private static ConfigData config = new ConfigData();

        public static void LoadConfig()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                config = JsonConvert.DeserializeObject<ConfigData>(json);
            }
            else
            {
                config = new ConfigData();
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(path, json);
            }

        }
    }
}
