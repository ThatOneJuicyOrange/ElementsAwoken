using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee.SpaceYoyos
{
    public class Mars : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;

            item.damage = 15;
            item.knockBack = 9f;

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.rare = 1;
            item.value = Item.sellPrice(0, 1, 0, 0);

            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.UseSound = SoundID.Item1;

            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("MarsP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mars");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Earth", 1);
            recipe.AddRecipeGroup("IronBar", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
