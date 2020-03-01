using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Infernace
{
    public class InfernaceBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 4;
            item.expert = true;
        }
        public override int BossBagNPC => mod.NPCType("Infernace");

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
            player.QuickSpawnItem(mod.ItemType("FireHeart"));
            int choice = Main.rand.Next(4);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("FireBlaster"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("FlareSword"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("InfernoVortex"));
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("FireHarpyStaff"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("InfernaceMask"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("InfernaceTrophy"));
            }
        }
    }
}
