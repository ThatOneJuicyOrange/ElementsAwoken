using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Stellarium
{
    public class StellariumHamaxe : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 62;
            item.height = 64;

            item.damage = 78;
            item.axe = 36;
            item.hammer = 150;

            item.useTime = 14;
            item.useAnimation = 14;

            item.useTurn = true;
            item.melee = true;
            item.autoReuse = true;

            item.useStyle = 1;
            item.knockBack = 4.5f;

            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 10;

            item.UseSound = SoundID.Item1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellarium Hamaxe");
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
