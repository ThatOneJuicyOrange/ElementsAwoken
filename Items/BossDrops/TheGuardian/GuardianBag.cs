using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheGuardian
{
    public class GuardianBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 5;
            bossBagNPC = mod.NPCType("TheGuardianFly");
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
            int choice = Main.rand.Next(3);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("Godslayer"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("InfernoStorm"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("TemplesWrath"));
            }
            player.QuickSpawnItem(mod.ItemType("FieryCore"));
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("TheGuadianMask"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("TheGuardianTrophy"));
            }
        }
    }
}
