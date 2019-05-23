using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Void
{
    public class EyeofAnnihilation : ModItem
    {

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Code2);
            item.useStyle = 5;
            item.damage = 167;
            item.width = 16;
            item.height = 16;
            item.rare = 11;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = 541;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("TheEyeP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Eye of Annihilation");
            Tooltip.SetDefault("");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
