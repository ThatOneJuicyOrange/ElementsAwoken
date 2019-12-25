using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Tech.Generators
{
    public class SoulConverter : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = 1;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Converter");
            Tooltip.SetDefault("Allows enemies to drop batteries which charge the player\nBatteries restore more energy with slain bosses");
        }
        public override void UpdateInventory(Player player)
        {
            PlayerEnergy energyPlayer = player.GetModPlayer<PlayerEnergy>();
            energyPlayer.soulConverter = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 24);
            recipe.AddRecipeGroup("EvilBar", 12);
            recipe.AddIngredient(null, "GoldWire", 8);
            recipe.AddIngredient(null, "Capacitor", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
