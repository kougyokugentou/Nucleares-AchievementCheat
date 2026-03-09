using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace NuclearesAchievementCheat
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        internal static AchievementConfig ModConfig;

        private void Awake()
        {
            Log = Logger;

            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded.");

            // Load config
            ModConfig = AchievementConfig.Load();

            // If already completed, do nothing
            if (ModConfig.done)
            {
                Log.LogInfo("Achievements already unlocked. Skipping Harmony patch.");
                return;
            }

            Log.LogInfo("Achievements not yet unlocked. Applying Harmony patch.");

            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            Log.LogInfo("Harmony patches applied.");
        }
    }
}