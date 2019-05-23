using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Elements.Sky
{
    public class SkyScraper : ModItem
    {
        public override void SetDefaults()
        {       
            item.damage = 32;
            item.melee = true;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.useAnimation = 12;
            item.useTime = 12;
            item.useStyle = 5;
            item.knockBack = 4.75f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.height = 60;
            item.width = 60;
            item.maxStack = 1;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 6;
            item.shoot = mod.ProjectileType("SkyScraperP");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sky Scraper");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
