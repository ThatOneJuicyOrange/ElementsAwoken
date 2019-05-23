using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Elemental
{
    public class Andromeda : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 70;

            item.damage = 190;

            item.useTurn = true;
            item.autoReuse = true;
            item.melee = true;

            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.knockBack = 5;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 20, 0, 0);

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("AndromedaBlast");
            item.shootSpeed = 10f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Andromeda");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(null, "VoidAshes", 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
