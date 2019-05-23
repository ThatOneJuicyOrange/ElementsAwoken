using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Testing
{
    public class GodlyTomato : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 38;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Godly Tomato");
            Tooltip.SetDefault("am froont? am vegetal?\nTESTING ITEM");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.immune = true;
        }
    }
}
