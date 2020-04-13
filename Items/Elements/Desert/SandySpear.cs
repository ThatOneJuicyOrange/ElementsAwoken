using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Elements.Desert
{
    public class SandySpear : ModItem
    {
        public override void SetDefaults()
        {       
            item.damage = 16;
            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.useAnimation = 22;
            item.useStyle = 5;
            item.useTime = 22;  
            item.knockBack = 3.75f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.height = 60;
            item.width = 60;
            item.maxStack = 1;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            item.shoot = mod.ProjectileType("SandySpearP");
            item.shootSpeed = 6f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandy Spear");
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
