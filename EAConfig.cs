using System;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;

namespace ElementsAwoken
{
    public static class Config
    {
        public static bool alchemistEnabled = true;
        public static bool labsEnabled = true;
        public static bool bossPrompts = true;
        public static bool screenshake = true;
        public static bool debugMode = false;

        static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "EA Config.json");

        static Preferences Configuration = new Preferences(ConfigPath);

        public static void Load()
        {
            //Reading the config file
            bool success = ReadConfig();

            if (!success)
            {
                ErrorLogger.Log("Failed to read Elements Awoken's config file! Recreating config...");
                CreateConfig();
            }
        }

        static bool ReadConfig()
        {
            if (Configuration.Load())
            {
                Configuration.Get("Alchemist", ref alchemistEnabled);
                Configuration.Get("Labs", ref labsEnabled);
                Configuration.Get("BossPrompts", ref bossPrompts);
                Configuration.Get("Screenshake", ref screenshake);
                Configuration.Get("DebugMode", ref debugMode);
                return true;
            }
            return false;
        }

        static void CreateConfig()
        {
            Configuration.Clear();
            Configuration.Put("Alchemist", alchemistEnabled);
            Configuration.Put("Labs", labsEnabled);
            Configuration.Put("BossPrompts", bossPrompts);
            Configuration.Put("Screenshake", screenshake);
            Configuration.Put("DebugMode", debugMode);
            Configuration.Save();
        }
    }
}