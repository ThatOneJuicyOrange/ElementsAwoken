using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Aqueous
{
    public class AqueousBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 11;
            bossBagNPC = mod.NPCType("Aqueous");
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
            int choice = Main.rand.Next(6);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("BrinyBuster"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("BubblePopper"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("HighTide"));
            }
			if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("OceansRazor"));
            }
            if (choice == 4)
            {
                player.QuickSpawnItem(mod.ItemType("TheWave"));
            }
            if (choice == 5)
            {
                player.QuickSpawnItem(mod.ItemType("Varee"));
            }
            player.QuickSpawnItem(mod.ItemType("AqueousMask"));
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("AqueousTrophy"));
            }
        }
    }
}
