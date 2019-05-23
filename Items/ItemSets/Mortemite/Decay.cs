using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Mortemite
{
    public class Decay : ModItem
    {

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Code2);
            item.useStyle = 5;
            item.damage = 150;
            item.width = 16;
            item.height = 16;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = 541;
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 10;
            item.useAnimation = 15;
            item.useTime = 15;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("DecayP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Decay");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MortemiteDust", 50);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
