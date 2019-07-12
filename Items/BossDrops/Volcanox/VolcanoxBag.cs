using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Volcanox
{
    public class VolcanoxBag : ModItem
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
        public override int BossBagNPC => mod.NPCType("Volcanox");

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
            player.QuickSpawnItem(mod.ItemType("CharredInsignia"));
            int choice = Main.rand.Next(5);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("Combustia"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("EmberBurst"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("FatesFlame"));
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("FirestarterStaff"));
            }
            if (choice == 4)
            {
                player.QuickSpawnItem(mod.ItemType("Hearth"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("VolcanoxMask"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("VolcanoxTrophy"));
            }
        }
    }
}
