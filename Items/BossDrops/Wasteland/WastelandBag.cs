using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Wasteland
{
    public class WastelandBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 2;
            item.expert = true;
        }
        public override int BossBagNPC => mod.NPCType("Wasteland");


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
                player.QuickSpawnItem(mod.ItemType("Pincer"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("ScorpionBlade"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("Stinger"));
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("ChitinStaff"));
            }
            player.QuickSpawnItem(mod.ItemType("VenomSample"));
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("WastelandMask"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("WastelandTrophy"));
            }
        }
    }
}
