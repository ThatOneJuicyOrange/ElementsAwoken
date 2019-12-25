using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.Potions
{
    public class PinkJelly : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;

            item.useTurn = true;
            item.consumable = true;

            item.useAnimation = 17;
            item.useTime = 17;
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;

            item.maxStack = 30;

            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;

            item.buffType = mod.BuffType("Energized");
            item.buffTime = 600;
            //item.potion = true;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pink Jelly");
            Tooltip.SetDefault("Very sweet\n50% increased movement speed\nLife regen increased by 5");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.PinkGel, 5);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
