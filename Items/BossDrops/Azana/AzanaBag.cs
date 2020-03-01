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
            item.expert = true;
        }
        public override int BossBagNPC => mod.NPCType("Azana");

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
            int choice = Main.rand.Next(6);
            if (choice == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<Anarchy>());
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(ModContent.ItemType<PurgeRifle>());
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(ModContent.ItemType<ChaoticImpaler>());
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(ModContent.ItemType<GleamOfAnnhialation>());
            }
            if (choice == 4)
            {
                player.QuickSpawnItem(ModContent.ItemType<Pandemonium>());
            }
            if (choice == 5)
            {
                player.QuickSpawnItem(ModContent.ItemType<AzanaMinionStaff>());
            }
            player.QuickSpawnItem(ModContent.ItemType<RingOfChaos>());
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<AzanaMask>());
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<AzanaTrophy>());
            }
            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<EntropicCoating>());
            }
            player.QuickSpawnItem(ModContent.ItemType<DiscordantOre>(), Main.rand.Next(15, 20));

        }
    }
}
