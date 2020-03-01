using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.Potions
{
    public class CalamityPotion : ModItem
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

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 9;

            item.buffType = mod.BuffType("CalamityPotionBuff");
            item.buffTime = 10800;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Calamity Potion");
            Tooltip.SetDefault("Increases spawn rates by 12.5x for 3 minutes");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaosPotion", 1);
            recipe.AddIngredient(ItemID.LunarOre, 1);
            recipe.AddIngredient(null, "NeutronFragment", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
