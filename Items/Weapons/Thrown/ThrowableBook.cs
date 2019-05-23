using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Thrown
{
    public class ThrowableBook : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;

            item.thrown = true;
            item.noMelee = true;
            item.consumable = true;
            item.noUseGraphic = true;

            item.knockBack = 2f;
            item.damage = 14;
            item.maxStack = 999;

            item.useAnimation = 13;
            item.useTime = 13;
            item.useStyle = 1;

            item.UseSound = SoundID.Item1;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 0, 1, 0);

            item.shoot = mod.ProjectileType("ThrowableBookP");
            item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwable Book");
            Tooltip.SetDefault("Such magnificient tragedy written within this book! It really hurts you!");
        }
    }
}
