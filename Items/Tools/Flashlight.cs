using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tools
{
    public class Flashlight : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.rare = 5;
            item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flashlight");
            Tooltip.SetDefault("Light up the world");
        }

        public override void HoldItem(Player player)
        {
            int dist = 15;
            Vector2 playerTile = player.Center / 16;
            Vector2 toMouse = Main.MouseWorld - player.Center;
            toMouse.Normalize();
            Vector2 mouseTile = playerTile + (toMouse * dist);
            for (float i = 0; i < 1; i += 1f / (float)dist)
            {
                Vector2 lightTile = Vector2.Lerp(playerTile, mouseTile, i);
                Lighting.AddLight(lightTile * 16, 0.5f * i, 0, 0);
            }
        }
    }
}
