using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Water
{
    public class WaterSword : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 72;
            item.damage = 70;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.useTime = 5;
            item.knockBack = 6.5f;
            item.autoReuse = false;
            item.height = 78;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8;
            item.shoot = mod.ProjectileType("WaterSwordP");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forever Tide");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
