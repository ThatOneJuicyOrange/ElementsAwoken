using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using ElementsAwoken.Structures;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Testing
{
    public class DemonsDoll : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = 4;
            item.UseSound = SoundID.Item60;
            item.consumable = false;
            item.GetGlobalItem<EATooltip>().testing = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon's Doll");
            Tooltip.SetDefault("help\nleft click to toggle hjardmode\nright click to toggle post moonlord");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (NPC.downedMoonlord)
                {
                    NPC.downedMoonlord = false;
                    Main.NewText("Terraria is fine lmao", Color.GreenYellow.R, Color.GreenYellow.G, Color.GreenYellow.B);
                }
                else
                {
                    NPC.downedMoonlord = true;
                    Main.NewText("Terraria is scared help", Color.GreenYellow.R, Color.GreenYellow.G, Color.GreenYellow.B);
                }
            }
            else
            {
                if (Main.hardMode)
                {
                    Main.hardMode = false;
                    MyWorld.awakenedPlateau = false;
                    Main.NewText("The ancient spirits of light and dark have been sealed away once more...", Color.GreenYellow.R, Color.GreenYellow.G, Color.GreenYellow.B);
                }
                else
                {
                    Main.hardMode = true;
                    MyWorld.awakenedPlateau = true;
                    Main.NewText("The ancient spirits of light and dark have been released.", Color.GreenYellow.R, Color.GreenYellow.G, Color.GreenYellow.B);
                }
            }
            return true;
        }
    }
}
