﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Placeable.VoidStone
{
    public class VoidPlatform : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 11;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 0;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("VoidPlatform");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Platform");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidBrick", 1);
            //recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }
    }
}
