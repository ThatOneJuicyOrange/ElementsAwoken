using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Permafrost
{
    public class IcyHeart : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.rare = 11;
            item.value = Item.sellPrice(0, 12, 50, 0);

            item.accessory = true;
            item.GetGlobalItem<EARarity>().awakened = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icy Heart");
            Tooltip.SetDefault("The player generates an ice shield after 10 seconds of not taking damage\nTakes 20 seconds of charging to block 100% of damage");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.icyHeart = true;
        }
    }
}
