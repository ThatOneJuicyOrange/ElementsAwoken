using System;
using System.ComponentModel;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace ElementsAwoken
{
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

        [Label("Disable Sanity Distortion")]
        [Tooltip("Stops the distortion when going insane")]
        public bool sanityDistortionOff { get; set; }

        [Label("Low Dust Mode")]
        [Tooltip("Reduces the amount of dust created by NPCs, projectiles and more (WARNING: only applied to certain objects)")]
        public bool lowDust { get; set; }

        [Label("Enter Debug Mode")]
        [Tooltip("Shows information for testing (Mainly for Mod Devs)")]
        public bool debugMode { get; set; }
    }
}