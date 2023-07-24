using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IOPath = System.IO.Path;

namespace _86BoxManager.Registry
{
    public static class Configs
    {
        private static readonly JsonSerializerSettings JsonConfig;
        private static readonly string BoxConfigName;
        private static readonly string VmxConfigName;

        static Configs()
        {
            JsonConfig = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            var userConfigDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create);
            var appConfigDir = IOPath.Combine(userConfigDir, "86BoxManager");
            if (!Directory.Exists(appConfigDir)) Directory.CreateDirectory(appConfigDir);

            BoxConfigName = IOPath.Combine(appConfigDir, "86Box.json");
            VmxConfigName = IOPath.Combine(appConfigDir, "86BoxVMs.json");
        }

        private static void WriteJson(string fileName, object obj)
        {
            var json = JsonConvert.SerializeObject(obj, JsonConfig);
            File.WriteAllText(fileName, json, Encoding.UTF8);
        }

        private static object ReadJson(string fileName)
        {
            if (!File.Exists(fileName))
                return null;
            var json = File.ReadAllText(fileName, Encoding.UTF8);
            return JsonConvert.DeserializeObject(json, JsonConfig);
        }

        public static void Create86BoxKey()
        {
            var obj = new JObject();
            WriteJson(BoxConfigName, obj);
        }

        public static void Create86BoxVmKey()
        {
            var obj = new JObject();
            WriteJson(VmxConfigName, obj);
        }

        public static ConfigKey Open86BoxKey(bool readWrite = false)
        {
            var obj = ReadJson(BoxConfigName);
            return obj == null
                ? null
                : new ConfigKey(obj, readWrite ? x => WriteJson(BoxConfigName, x) : null);
        }

        public static ConfigKey Open86BoxVmKey(bool readWrite = false)
        {
            var obj = ReadJson(VmxConfigName);
            return obj == null
                ? null
                : new ConfigKey(obj, readWrite ? x => WriteJson(VmxConfigName, x) : null);
        }
    }
}