using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Accessories
{
    public class HellbatWing : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 3;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellbat Wing");
            Tooltip.SetDefault("Jump speed increased by 200%\nCrit chance increased by 5%");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 2.0f;
            player.meleeCrit += 5;
            player.magicCrit += 5;
            player.rangedCrit += 5;
            player.thrownCrit += 5;

        }
    }
}
