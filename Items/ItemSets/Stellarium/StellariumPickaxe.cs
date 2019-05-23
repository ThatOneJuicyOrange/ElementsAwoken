using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Stellarium
{
    public class StellariumPickaxe : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;

            item.damage = 85;
            item.pick = 230;
            item.knockBack = 4.5f;

            item.useStyle = 1;
            item.useTime = 8;
            item.useAnimation = 11;
            item.UseSound = SoundID.Item1;

            item.useTurn = true;
            item.autoReuse = true;
            item.melee = true;

            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 10;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellarium Pickaxe");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "StellariumBar", 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
