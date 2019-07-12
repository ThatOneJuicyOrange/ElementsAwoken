using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    public class VoidLeviathanBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.expert = true;
        }
        public override int BossBagNPC => mod.NPCType("VoidLeviathanHead");

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
            int choice = Main.rand.Next(8);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("BladeOfTheNight"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("CosmicWrath"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("EndlessAbyssBlaster"));
            }
			if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("ExtinctionBow"));
            }
            if (choice == 4)
            {
                player.QuickSpawnItem(mod.ItemType("PikeOfEternalDespair"));
            }
            if (choice == 5)
            {
                player.QuickSpawnItem(mod.ItemType("Reaperstorm"));
            }
            if (choice == 6)
            {
                player.QuickSpawnItem(mod.ItemType("VoidInferno"));
            }
            if (choice == 7)
            {
                player.QuickSpawnItem(mod.ItemType("VoidLeviathanAegis"));
            }

            player.QuickSpawnItem(mod.ItemType("VoidLeviathanHeart"), 2);
            player.QuickSpawnItem(mod.ItemType("AmuletOfDestruction"));
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("VoidLeviathanMask"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("VoidLeviathanTrophy"));
            }
        }
    }
}
