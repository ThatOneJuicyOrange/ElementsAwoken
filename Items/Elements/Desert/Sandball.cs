using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Desert
{
    public class Sandball : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;

            item.damage = 14;
            item.knockBack = 2f;

            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.consumable = true;
            item.autoReuse = true;

            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.UseSound = SoundID.Item39;

            item.maxStack = 999;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("Sandball");
            item.shootSpeed = 13f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandball");
            Tooltip.SetDefault("I don't like sand. It's coarse and rough and irritating and it gets everywhere");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEssence", 4);
            recipe.AddRecipeGroup("ElementsAwoken:SandGroup", 25);
            recipe.AddRecipeGroup("ElementsAwoken:SandstoneGroup", 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this, 450);
            recipe.AddRecipe();
        }
    }
}
