using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Desert
{
    public class TheGrit : ModItem
    {

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Code2);
            item.damage = 25;

            item.useStyle = 5;
            item.width = 16;
            item.height = 16;
            item.rare = 3;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = 541;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("TheGritP");
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("The Grit");
      Tooltip.SetDefault("");
    }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEssence", 4);
            recipe.AddRecipeGroup("ElementsAwoken:SandGroup", 25);
            recipe.AddRecipeGroup("ElementsAwoken:SandstoneGroup", 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
