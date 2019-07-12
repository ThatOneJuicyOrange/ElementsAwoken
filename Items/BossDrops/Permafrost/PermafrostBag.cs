using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Permafrost
{
    public class PermafrostBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 7;
            item.expert = true;
        }
        public override int BossBagNPC => mod.NPCType("Permafrost");

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
                player.QuickSpawnItem(mod.ItemType("IceReaver"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("Snowdrift"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("IceWrath"));
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("Flurry"));
            }
            player.QuickSpawnItem(mod.ItemType("SoulOfTheFrost"));
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("PermafrostMask"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("PermafrostTrophy"));
            }
        }
    }
}
