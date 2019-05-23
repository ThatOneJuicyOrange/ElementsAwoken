using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Floral
{
    public class BlossomBoomer : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;

            item.damage = 15;

            item.thrown = true;
            item.noMelee = true;
            item.consumable = true;
            item.noUseGraphic = true;
            item.autoReuse = false;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.knockBack = 3f;
            item.UseSound = SoundID.Item1;
            item.maxStack = 999;

            item.value = Item.buyPrice(0, 2, 0, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("BlossomBoomerP");
            item.shootSpeed = 6.5f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blossom Boomer");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Petal", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }
}
