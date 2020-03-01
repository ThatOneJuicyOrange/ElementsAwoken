using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Items.Materials;

namespace ElementsAwoken.Items.Consumable
{
    public class VoidbloodHeart : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item119;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidblood Heart");
            Tooltip.SetDefault("Disabled all natural regeneration\nHealing potions are disabled\nThe nurse costs 3x more and is disabled when injured and during bossfights\nYou leak toxic Voidblood on hit and low health");
        }

        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<MyPlayer>().voidBlood)
            {
                Main.NewText("Your veins return to normal.", Color.DarkRed);
                player.GetModPlayer<MyPlayer>().voidBlood = false;
            }
            else
            {
                Main.NewText("Your veins turn black.", Color.DarkRed);
                player.GetModPlayer<MyPlayer>().voidBlood = true;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimsonSeeds, 1);
            recipe.AddIngredient(ItemType<Stardust>(), 2);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe(); 
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CorruptSeeds, 1);
            recipe.AddIngredient(ItemType<Stardust>(), 2);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
