using System;
using System.IO;
using Newtonsoft.Json;
using TaleWorlds.Library;

namespace AutoSettlementBuilder.Config
{
    public static class ConfigManager
    {
        private static readonly object LockObject = new object();
        private static ModConfig _config;

        public static ModConfig Config
        {
            get
            {
                if (_config == null)
                {
                    lock (LockObject)
                    {
                        if (_config == null)
                        {
                            Initialize();
                        }
                    }
                }
                return _config;
            }
        }

        public static void Initialize()
        {
            lock (LockObject)
            {
                string path = GetConfigFilePath();
                try
                {
                    if (File.Exists(path))
                    {
                        string json = File.ReadAllText(path);
                        _config = JsonConvert.DeserializeObject<ModConfig>(json) ?? new ModConfig();
                    }
                    else
                    {
                        _config = new ModConfig();
                        SaveConfig(path, _config);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print($"[AutoSettlementBuilder] Error initializing config at '{path}': {ex.Message}");
                    _config = new ModConfig();
                }
            }
        }

        public static void Save()
        {
            lock (LockObject)
            {
                if (_config != null)
                {
                    string path = GetConfigFilePath();
                    SaveConfig(path, _config);
                }
            }
        }

        private static void SaveConfig(string path, ModConfig config)
        {
            try
            {
                string directory = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Debug.Print($"[AutoSettlementBuilder] Error saving config to '{path}': {ex.Message}");
            }
        }

        public static string GetConfigFilePath()
        {
            try
            {
                string assemblyLocation = typeof(ConfigManager).Assembly.Location;
                if (!string.IsNullOrEmpty(assemblyLocation))
                {
                    string binDir = Path.GetDirectoryName(assemblyLocation);
                    if (!string.IsNullOrEmpty(binDir))
                    {
                        string moduleDir = Path.GetFullPath(Path.Combine(binDir, "..", ".."));
                        return Path.Combine(moduleDir, "ModuleData", "config.json");
                    }
                }
            }
            catch
            {
            }

            try
            {
                if (!string.IsNullOrEmpty(BasePath.Name))
                {
                    return Path.Combine(BasePath.Name, "Modules", "AutoSettlementBuilder", "ModuleData", "config.json");
                }
            }
            catch
            {
            }

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules", "AutoSettlementBuilder", "ModuleData", "config.json");
        }
    }
}
