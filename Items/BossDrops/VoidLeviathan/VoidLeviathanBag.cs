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
            Tooltip.SetDefault("Right Click to open");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
            int choice = Main.rand.Next(10);
            if (choice == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<BladeOfTheNight>());
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(ModContent.ItemType<CosmicWrath>());
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(ModContent.ItemType<EndlessAbyssBlaster>());
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(ModContent.ItemType<ExtinctionBow>());
            }
            if (choice == 4)
            {
                player.QuickSpawnItem(ModContent.ItemType<PikeOfEternalDespair>());
            }
            if (choice == 5)
            {
                player.QuickSpawnItem(ModContent.ItemType<Reaperstorm>());
            }
            if (choice == 6)
            {
                player.QuickSpawnItem(ModContent.ItemType<VoidInferno>());
            }
            if (choice == 7)
            {
                player.QuickSpawnItem(ModContent.ItemType<VoidLeviathansAegis>());
            }
            if (choice == 8)
            {
                player.QuickSpawnItem(ModContent.ItemType<BreathOfDarkness>());
            }
            if (choice == 9)
            {
                player.QuickSpawnItem(ModContent.ItemType<LightsAffliction>());
            }
            player.QuickSpawnItem(ModContent.ItemType<VoidLeviathanHeart>(),2);
            player.QuickSpawnItem(ModContent.ItemType<AmuletOfDestruction>());
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<VoidLeviathanMask>());
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<VoidLeviathanTrophy>());
            }
            if (MyWorld.awakenedMode)
            {
                player.QuickSpawnItem(ModContent.ItemType<AbyssalMatter>());
            }
        }
    }
}
