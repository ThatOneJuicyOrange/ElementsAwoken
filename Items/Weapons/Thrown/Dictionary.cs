using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Thrown
{
    public class Dictionary : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;

            item.thrown = true;
            item.noMelee = true;
            item.consumable = true;
            item.noUseGraphic = true;

            item.knockBack = 8f;
            item.damage = 36;
            item.maxStack = 999;

            item.useAnimation = 8;
            item.useTime = 8;
            item.useStyle = 1;

            item.UseSound = SoundID.Item1;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 0, 8, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("DictionaryP");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dictionary");
            Tooltip.SetDefault("Sticks and stones may break my bones but words will never hurt me...");
        }
    }
}
