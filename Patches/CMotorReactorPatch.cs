using HarmonyLib;
using System.Reflection;

namespace NuclearesAchievementCheat.Patches
{
    [HarmonyPatch]
    class Patch_MasaCritica
    {
        internal static AchievementConfig ModConfig;

        static MethodBase TargetMethod()
        {
            var type = AccessTools.TypeByName("CMotorReactor+CEventosInternos");

            if (type == null)
            {
                Plugin.Log.LogError("Type CMotorReactor+CEventosInternos not found");
                return null;
            }

            var method = AccessTools.Method(type, "<RutinaActualizando>g__MasaCritica|48_4");

            if (method == null)
            {
                Plugin.Log.LogError("<RutinaActualizando>g__MasaCritica|48_4 method not found");
            }
            else
            {
                Plugin.Log.LogInfo("<RutinaActualizando>g__MasaCritica|48_4 method patched");
            }

            return method;
        }

        static void Prefix()
        {
            // Load config
            ModConfig = AchievementConfig.Load();

            // If already completed, do nothing
            if (ModConfig.done)
            {
                return;
            }
            
            Plugin.Log.LogInfo("Unlocking all achievements...");

            foreach (CHistoria.CLogros.LogrosSteam achievement in 
                    System.Enum.GetValues(typeof(CHistoria.CLogros.LogrosSteam)))
            {
                try
                {
                    Plugin.Log.LogInfo($"Unlocking achievement: {achievement}");

                    CHistoria.CLogros.LogroDesbloqueado(achievement);

                    Plugin.Log.LogInfo($"Successfully unlocked: {achievement}");
                }
                catch (System.Exception ex)
                {
                    Plugin.Log.LogError($"Failed to unlock achievement: {achievement}");
                    Plugin.Log.LogError(ex);
                }
            }

            Plugin.Log.LogInfo("Achievement unlock routine finished.");
            ModConfig.done = true;
            ModConfig.Save();
        }
    }
}