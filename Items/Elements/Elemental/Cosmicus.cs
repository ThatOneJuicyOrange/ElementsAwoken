using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Elemental
{
    public class Cosmicus : ModItem
    {
        public int charge = 0;
        public override void SetDefaults()
        {
            item.width = 38;  
            item.height = 38;
            item.damage = 210;

            item.useAnimation = 7;
            item.useStyle = 1;
            item.useTime = 7;
            item.knockBack = 7.5f;
            item.UseSound = SoundID.Item1;

            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.thrown = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 20, 0, 0);

            item.shoot = mod.ProjectileType("CosmicusP");
            item.shootSpeed = 16f;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmicus");
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
