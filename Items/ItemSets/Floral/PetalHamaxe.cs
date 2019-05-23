using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Floral
{
    public class PetalHamaxe : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 60;

            item.damage = 10;
            item.axe = 30;
            item.hammer = 65;
            item.knockBack = 4.5f;

            item.useStyle = 1;
            item.useTime = 20;
            item.useAnimation = 20;
            item.UseSound = SoundID.Item1;

            item.melee = true;       
            item.useTurn = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 2, 0, 0);
            item.rare = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Petal Hamaxe");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Petal", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
