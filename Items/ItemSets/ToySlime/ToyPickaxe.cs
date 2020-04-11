using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.ToySlime
{
    public class ToyPickaxe : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 60;
            
            item.damage = 11;
            item.pick = 95;
            item.knockBack = 4.5f;

            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.useTurn = true;
            item.melee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toy Pickaxe");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<BrokenToys>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
