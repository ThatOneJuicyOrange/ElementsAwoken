using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Consumable.Potions
{
    public class CelestialEmpowerment : ModItem
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
            item.value = 15000;
            item.rare = 9;
            item.buffType = mod.BuffType("LuminiteBuff");
            item.buffTime = 7200;
            return;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Empowerment");
            Tooltip.SetDefault("Increase defense by 8\n15% increased damage");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(null, "Pyroplasm", 2);
            recipe.AddIngredient(ItemID.LunarOre, 4);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
