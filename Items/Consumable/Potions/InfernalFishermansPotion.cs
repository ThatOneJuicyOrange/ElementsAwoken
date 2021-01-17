using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Consumable.Potions
{
    public class InfernalFishermansPotion : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;

            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useAnimation = 30;
            item.useTime = 30;

            item.useTurn = true;
            item.consumable = true;

            item.maxStack = 30;

            item.value = Item.sellPrice(0, 0, 8, 0);
            item.rare = 3;

            item.buffType = BuffType<Buffs.PotionBuffs.LavaFishingBuff>();
            item.buffTime = 10800;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Fisherman's Potion");
            Tooltip.SetDefault("Allows the player to fish in lava with any rod");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Fireblossom, 1);
            recipe.AddIngredient(ItemID.ArmoredCavefish, 1);
            recipe.AddIngredient(null, "MagmaCrystal", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
