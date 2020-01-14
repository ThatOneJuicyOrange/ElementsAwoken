using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.Potions
{
    public class SanityRegenerationPotion : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;

            item.maxStack = 30;

            item.consumable = true;
            item.useTurn = true;

            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;

            item.value = Item.sellPrice(0, 0, 2, 0);
            item.rare = 3;

            item.buffType = mod.BuffType("SanityRegenerationBuff");
            item.buffTime = 7200;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sanity Regeneration Potion");
            Tooltip.SetDefault("Increases sanity regeneration");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddRecipeGroup("ElementsAwoken:EvilOre", 4);
            recipe.AddIngredient(ItemID.SpecularFish, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
