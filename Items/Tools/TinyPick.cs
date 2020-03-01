using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tools
{
    public class TinyPick : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 3;
            item.melee = true;
            item.width = 56;
            item.height = 60;
            item.useTime = 4;
            item.useAnimation = 4;
            item.useTurn = true;
            item.pick = 20;
            item.useStyle = 1;
            item.knockBack = 2f;
            item.value = Item.buyPrice(0, 0, 40, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tiny Pick");
            Tooltip.SetDefault("How is this even functional?");
        }
    }
}
