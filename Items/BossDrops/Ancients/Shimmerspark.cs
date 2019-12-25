using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ElementsAwoken.Items.BossDrops.Ancients
{
    public class Shimmerspark : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 34;

            item.damage = 1750;
            item.knockBack = 12f;

            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;

            item.noMelee = true;
            item.autoReuse = false;
            item.ranged = true;
            item.channel = true;
            item.noUseGraphic = true;

            item.value = Item.sellPrice(0, 75, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 14;

            item.shoot = mod.ProjectileType("ShimmersparkHeld");
            item.shootSpeed = 20f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shimmerspark");
            Tooltip.SetDefault("Hold down shoot to charge a reality breaking blast");
        }

    }
}
