using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using ElementsAwoken.Buffs.Debuffs;

namespace ElementsAwoken.Items.Tech.Generators
{
    public class LunarWaste : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = -1;

            item.maxStack = 999;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Waste");
            Tooltip.SetDefault("Reduces life regen if too much builds up\nWill start damaging the player");
        }
        public override void UpdateInventory(Player player)
        {
            if (item.stack >= 10)
            {
                player.AddBuff(ModContent.BuffType<Irradiated>(), 20);
                player.lifeRegen--;
            }
            if (item.stack >= 15)
            {
                player.lifeRegen -= 2;
            }
            if (item.stack >= 20)
            {
                player.lifeRegen -= 3;
            }
            if (item.stack >= 30)
            {
                player.lifeRegen -= 5;
            }
            if (item.stack >= 40)
            {
                player.lifeRegen -= 10;
            }
            if (item.stack >= 50)
            {
                player.lifeRegen -= 20;
            }
        }
    }
}
