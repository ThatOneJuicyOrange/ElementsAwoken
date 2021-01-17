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
            Tooltip.SetDefault("Right Click to open");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            int choice = Main.rand.Next(4);
            if (choice == 0) player.QuickSpawnItem(ModContent.ItemType<Pincer>());
            else if (choice == 1) player.QuickSpawnItem(ModContent.ItemType<ScorpionBlade>());
            else if (choice == 2) player.QuickSpawnItem(ModContent.ItemType<Stinger>());
            else if (choice == 3) player.QuickSpawnItem(ModContent.ItemType<ChitinStaff>());

            player.QuickSpawnItem(ModContent.ItemType<VenomSample>());
            if (MyWorld.awakenedMode) player.QuickSpawnItem(ModContent.ItemType<TheAntidote>());

            if (Main.rand.Next(10) == 0) player.QuickSpawnItem(ModContent.ItemType<WastelandMask>());
            if (Main.rand.Next(10) == 0) player.QuickSpawnItem(ModContent.ItemType<WastelandTrophy>());
        }
    }
}
