using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Floral
{
    public class BudKunai : ModItem
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

            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.knockBack = 3f;
            item.UseSound = SoundID.Item1;
            item.maxStack = 999;

            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("BudKunaiP");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bud Kunai");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Petal", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
