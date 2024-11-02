using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppMicrosoft.Xbox;


namespace TaikoGamerTag
{
    [BepInPlugin(Plugin.GUID, "Taiko Gamer Tag", "1.0.0")]
    public class Plugin : BasePlugin
    {
        internal const string GUID = "fr.minemobs.taikogamertag";
        internal static ConfigEntry<string> gamerTag;
        internal static ManualLogSource LOGGER;
        public override void Load()
        {
            LOGGER = Log;
            Log.LogInfo($"Plugin {GUID} is loaded!");
            gamerTag = Config.Bind("Config", "GamerTag", "", "A name to replace your Gamer Tag (only useful when your gametag is Razor1911)");
            Harmony.CreateAndPatchAll(typeof(Plugin));
        }

        [HarmonyPatch(typeof (GdkHelpers), nameof(GdkHelpers.GetGamerTag))]
        public static bool Prefix(ref string __result)
        {
            if(gamerTag.Value.Length == 0) return true;
            __result = gamerTag.Value;
            LOGGER.LogInfo($"New GamerTag is {__result}");
            return false;
        }
    }
}
