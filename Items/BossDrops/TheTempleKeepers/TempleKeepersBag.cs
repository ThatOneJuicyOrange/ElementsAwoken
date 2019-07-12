using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheTempleKeepers
{
    public class TempleKeepersBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 5;
            item.expert = true;
        }
        public override int BossBagNPC => mod.NPCType("TheEye");

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
            player.TryGettingDevArmor();
            int choice = Main.rand.Next(4);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("TemplesCrystal"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("GazeOfInferno"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("TheAllSeer"));
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("WyrmClaw"));
            }
            player.QuickSpawnItem(mod.ItemType("Flare"));
            player.QuickSpawnItem(mod.ItemType("WyrmHeart"));
            player.QuickSpawnItem(mod.ItemType("TempleFragment"));
        }
    }
}
