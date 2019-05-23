using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Developer
{
    public class BugBasher : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 210;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useAnimation = 18;
            item.useStyle = 1;
            item.useTime = 18;
            item.knockBack = 10f;
            item.UseSound = SoundID.Item1;
            item.thrown = true;
            item.height = 68;
            item.width = 56;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.shoot = mod.ProjectileType("BugBasherP");
            item.shootSpeed = 10f;

            item.GetGlobalItem<EATooltip>().developer = true;
            item.GetGlobalItem<EARarity>().rare = 12;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bug Basher");
            Tooltip.SetDefault("Crush the pests of Terraria\nData Crusader's developer weapon");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(null, "NeutronFragment", 8);
            recipe.AddIngredient(null, "VoidAshes", 8);
            recipe.AddIngredient(ItemID.ChlorophyteWarhammer, 1);
            recipe.AddIngredient(ItemID.Buggy, 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
