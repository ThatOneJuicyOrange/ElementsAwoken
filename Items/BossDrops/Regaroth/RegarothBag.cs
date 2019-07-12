using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Regaroth
{
    public class RegarothBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;

            item.consumable = true;
            item.expert = true;

            item.width = 24;
            item.height = 24;
            item.rare = 5;
        }
        public override int BossBagNPC => mod.NPCType("RegarothHead");

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
                player.QuickSpawnItem(mod.ItemType("EyeOfRegaroth"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("Starstruck"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("TheSilencer"));
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("EnergyStaff"));
            }
            player.QuickSpawnItem(mod.ItemType("StoneOfHope"));
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("RegarothMask"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("RegarothTrophy"));
            }
            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("EnergyWeaversHelm"));
                player.QuickSpawnItem(mod.ItemType("EnergyWeaversBreastplate"));
                player.QuickSpawnItem(mod.ItemType("EnergyWeaversLeggings"));
            }
        }
    }
}
