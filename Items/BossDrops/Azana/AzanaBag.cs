using System;
using System.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class AzanaBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 11;
            bossBagNPC = mod.NPCType("Azana");
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
            int choice = Main.rand.Next(5);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("Anarchy"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("PurgeRifle"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("ChaoticImpaler"));
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("GleamOfAnnhialation"));
            }
            if (choice == 4)
            {
                player.QuickSpawnItem(mod.ItemType("Pandemonium"));
            }
            player.QuickSpawnItem(mod.ItemType("RingOfChaos"));
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("AzanaMask"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("AzanaTrophy"));
            }
            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("EntropicCoating"));
            }
            player.QuickSpawnItem(mod.ItemType("DiscordantBar"), Main.rand.Next(15, 25));

        }
    }
}
