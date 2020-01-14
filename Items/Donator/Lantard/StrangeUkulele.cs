using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Lantard
{
    public class StrangeUkulele : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;

            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 20, 0);

            item.accessory = true;

            item.defense = 2;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Ukulele");
            Tooltip.SetDefault("'And his music was electric'\nUpon striking an enemy, lightning may arc to nearby enemies dealing 50% of the weapons damage\nLantard's donator item");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.strangeUkulele = true;

            player.buffImmune[mod.BuffType("ChaosBurn")] = true;

            player.armorPenetration += 10;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Wood", 35);
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
