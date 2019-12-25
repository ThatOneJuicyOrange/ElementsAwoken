using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheTempleKeepers
{
    public class Flare : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;

            item.accessory = true;
            item.expert = true;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flare");
            Tooltip.SetDefault("Pressing the ability key will create a shield around the player that pushes enemies and projectiles away");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.flare = true;
        }
    }
}
