using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Desert
{
    public class Sandshot : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 11;
            item.ranged = true;
            item.width = 58;
            item.height = 22;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 2.25f;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 12f;
            item.useAmmo = 40;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandshot");
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
