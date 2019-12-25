using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Superbaseball101
{
    public class HeartContainer : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 5;    

            item.accessory = true;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart Container");
            Tooltip.SetDefault("Increases your max number of minions by 1\nMinions melee attacks have a small chance to steal health from enemies\nSuperbaseball101's donator item");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.heartContainer = true;
            player.maxMinions += 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LifeCrystal, 3);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.PixieDust, 20);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
