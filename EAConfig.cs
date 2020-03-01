using System;
using System.ComponentModel;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace ElementsAwoken
{
    /*public static class Config
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
    }*/
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Enable Alchemist Potion Shop")]
        [Tooltip("Enables the Alchemists potion shop, replacing the potion material shop")]
        public bool alchemistPotions { get; set; }

        [Label("Resource Bars")]
        [Tooltip("Turns the energy and insanity UI into bars")]
        public bool resourceBars { get; set; }

        [Label("Disable Labs")]
        [Tooltip("Stops Labs from generating on world gen")]
        [ReloadRequired]
        public bool labsDisabled { get; set; }

        [Label("Disable Boss Prompts")]
        [Tooltip("Disables the effects that happen in the world 30 minutes after beating a boss")]
        public bool promptsDisabled { get; set; }

        [Label("Disable Vanilla Item Changes")]
        [Tooltip("Disabled all changes to vanilla items")]
        public bool vItemChangesDisabled { get; set; }

        [Label("Disable Screenshake")]
        public bool screenshakeDisabled { get; set; }

        [Label("Low Dust Mode")]
        [Tooltip("Reduces the amount of dust created by NPCs, projectiles and more (WARNING: only applied to certain objects)")]
        public bool lowDust { get; set; }

        [Label("Enter Debug Mode")]
        [Tooltip("Shows information for testing (Mainly for Mod Devs)")]
        public bool debugMode { get; set; }
    }
}