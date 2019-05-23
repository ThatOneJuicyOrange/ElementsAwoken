using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ScourgeFighter
{
    public class ScourgeFighterBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            bossBagNPC = mod.NPCType("ScourgeFighter");
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
            int choice = Main.rand.Next(4);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("ScourgeSword"));      
            }
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("SignalBooster"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("ScourgeFighterMachineGun"));
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("ScourgeFighterRocketLauncher"));
                player.QuickSpawnItem(ItemID.RocketI, Main.rand.Next(50, 150));
            }
            player.QuickSpawnItem(mod.ItemType("ScourgeDrive"));
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("ScourgeFighterMask"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("ScourgeFighterTrophy"));
            }
        }
    }
}
