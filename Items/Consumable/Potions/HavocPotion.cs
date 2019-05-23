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
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 30;
            item.consumable = true;
            item.width = 20;
            item.height = 28;
            item.value = 2500;
            item.rare = 3;
            item.buffType = mod.BuffType("HavocPotionBuff");
            item.buffTime = 10800;
            //item.potion = true;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Havoc Potion");
            Tooltip.SetDefault("Increases spawn rates by 15");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BattlePotion, 1);
            recipe.AddIngredient(null, "ImpEar", 1);
            recipe.AddIngredient(ItemID.Hellstone, 1);
            recipe.AddIngredient(ItemID.ViciousPowder, 3);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BattlePotion, 1);
            recipe.AddIngredient(null, "ImpEar", 1);
            recipe.AddIngredient(ItemID.Hellstone, 1);
            recipe.AddIngredient(ItemID.VilePowder, 3);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
