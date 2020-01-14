using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Frost
{
    public class GlacialBlitz : ModItem
    {

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Code2);
            item.damage = 50;
            item.useStyle = 5;
            item.width = 16;
            item.height = 16;
            item.rare = 7;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = 541;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("BlitzP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacial Blitz");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FrostEssence", 7);
            recipe.AddRecipeGroup("ElementsAwoken:IceGroup", 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
