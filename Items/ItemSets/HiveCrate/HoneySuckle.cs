using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.ItemSets.HiveCrate
{
    public class Honeysuckle : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honeysuckle");
            Tooltip.SetDefault("Increases life and mana regen by 1\nIncreases the duration of Honey\nIncreases maximum life by 10");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 1;
            player.manaRegen += 1;
            player.statLifeMax2 += 10;
            if (player.honeyWet)
            {
                player.AddBuff(BuffID.Honey, 2700);
            }
        }
    }
}
