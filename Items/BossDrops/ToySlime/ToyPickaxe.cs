using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.ToySlime
{
    public class ToyPickaxe : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 6;
            item.melee = true;
            item.width = 56;
            item.height = 60;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useTurn = true;
            item.pick = 50;
            item.useStyle = 1;
            item.knockBack = 4.5f;
            item.value = Item.buyPrice(0, 0, 10, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Pickaxe");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BrokenToys", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
