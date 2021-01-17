using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.Potions
{
    public class DiscordantPotion : ModItem
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

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.buffType = mod.BuffType("DiscordantBuff");
            item.buffTime = 7200;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaosporidic Brew");
            Tooltip.SetDefault("Decreases the duration of 'Chaos State' by 50%");
        }
        public override void AddRecipes() 
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(null, "ChaoticFlare", 2);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
