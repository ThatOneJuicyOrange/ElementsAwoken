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
            item.expert = true;
        }
        public override int BossBagNPC => mod.NPCType("TheCelestial");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right Click to open");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
            int choice = Main.rand.Next(4);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("CelestialInferno"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("Celestia"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("EyeballStaff"));
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("Solus"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("TheCelestialTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("CelestialsMask"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("CelestialCrown"));
            }
            player.QuickSpawnItem(mod.ItemType("CelestialFlame"));
        }
    }
}
