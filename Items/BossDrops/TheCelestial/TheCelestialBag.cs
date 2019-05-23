using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheCelestial
{
    public class TheCelestialBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 6;
            bossBagNPC = mod.NPCType("TheCelestial");
            item.expert = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right click to open");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            int choice = Main.rand.Next(3);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("CelestialInferno"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("EyeOfTheCelestial"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("EyeballStaff"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("TheCelestialTrophy"));
            }
            player.QuickSpawnItem(mod.ItemType("CelestialFlame"));
        }
    }
}
