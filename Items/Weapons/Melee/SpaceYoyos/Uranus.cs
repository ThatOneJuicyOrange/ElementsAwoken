using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee.SpaceYoyos
{
    public class Uranus : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;

            item.useStyle = 5;
            item.damage = 31;
            item.knockBack = 5f;

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.rare = 4;
            item.value = Item.sellPrice(0, 7, 50, 0);

            item.useAnimation = 25;
            item.useTime = 25;
            item.UseSound = SoundID.Item1;

            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("UranusP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranus");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Venus", 1);
            recipe.AddIngredient(null, "DemonicFleshClump", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
