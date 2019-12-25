using System;
using System.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Ancients
{
    public class AncientsBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 11;
            item.expert = true;
        }
        public override int BossBagNPC => mod.NPCType("AncientAmalgamDeath");

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
            int choice = Main.rand.Next(3);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("Chromacast"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("Shimmerspark"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("TheFundamentals"));
            }
            player.QuickSpawnItem(mod.ItemType("CrystallineLocket"));
            if (MyWorld.awakenedMode)
            {
                player.QuickSpawnItem(mod.ItemType("GiftOfTheArchaic"));
            }
            if (Main.rand.Next(10) == 0)
            {
                //player.QuickSpawnItem(mod.ItemType("AncientsMask"));
            }
            if (Main.rand.Next(10) == 0)
            {
                //player.QuickSpawnItem(mod.ItemType("AncientsTrophy"));
            }
            player.QuickSpawnItem(mod.ItemType("DiscordantOre"), Main.rand.Next(45, 90));

        }
    }
}
