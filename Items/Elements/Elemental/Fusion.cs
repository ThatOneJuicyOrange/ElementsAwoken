using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Elements.Elemental
{
    public class Fusion : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;

            item.damage = 220;
            item.crit = 20;

            item.noUseGraphic = true;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 20, 0, 0);

            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.UseSound = SoundID.Item1;

            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("FusionP");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fusion");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(null, "VoidAshes", 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
