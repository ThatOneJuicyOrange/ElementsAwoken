using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Water
{
    public class ChakramOfTheDuke : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 38;  
            item.height = 38;
            item.damage = 70;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useAnimation = 7;
            item.useStyle = 1;
            item.useTime = 7;
            item.knockBack = 7.5f;
            item.UseSound = SoundID.Item1;
            item.thrown = true;
            item.value = Item.buyPrice(0, 75, 0, 0);
            item.rare = 8; 
            item.shoot = mod.ProjectileType("DukeChakramProj");
            item.shootSpeed = 16f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chakram of the Duke");
            Tooltip.SetDefault("Tear your enemies to pieces");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
