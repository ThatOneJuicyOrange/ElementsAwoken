using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.Potions
{
    public class HavocPotion : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;

            item.useTurn = true;
            item.consumable = true;

            item.maxStack = 30;

            item.value = Item.sellPrice(0,0,4,0);
            item.rare = 3;

            item.buffType = mod.BuffType("HavocPotionBuff");
            item.buffTime = 10800;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Havoc Potion");
            Tooltip.SetDefault("Increases spawn rates by 4x for 3 minutes");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BattlePotion, 1);
            recipe.AddIngredient(null, "ImpEar", 1);
            recipe.AddIngredient(ItemID.Hellstone, 1);
            recipe.AddIngredient(ItemID.ViciousPowder, 3);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BattlePotion, 1);
            recipe.AddIngredient(null, "ImpEar", 1);
            recipe.AddIngredient(ItemID.Hellstone, 1);
            recipe.AddIngredient(ItemID.VilePowder, 3);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
