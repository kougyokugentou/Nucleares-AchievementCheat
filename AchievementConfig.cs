using System;
using System.IO;
using BepInEx;

namespace NuclearesAchievementCheat
{
    public class AchievementConfig
    {
        public bool done = false;

        public static string ConfigPath =>
            Path.Combine(Paths.ConfigPath, "Nucleares.AchievementCheat.config");

        public static AchievementConfig Load()
        {
            try
            {
                var config = new AchievementConfig();           
                     
                if (!File.Exists(ConfigPath))
                {
                    Plugin.Log.LogInfo("Config file not found, creating default.");
        
                    config.done = false;
                    config.Save(); // write file immediately
                    return config;
                }

                string text = File.ReadAllText(ConfigPath);

                if (text.Contains("\"done\": true"))
                    config.done = true;

                Plugin.Log.LogInfo($"Config loaded. done={config.done}");

                return config;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError("Failed to load config");
                Plugin.Log.LogError(ex);
                return new AchievementConfig();
            }
        }

        public void Save()
        {
            try
            {
                string json =
$@"{{
  ""done"": {done.ToString().ToLower()}
}}";

                File.WriteAllText(ConfigPath, json);

                Plugin.Log.LogInfo($"Config saved: {ConfigPath}");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError("Failed to save config");
                Plugin.Log.LogError(ex);
            }
        }
    }
}