/*using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Developer
{
    public class SilvestreGreatsword : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 290;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 14;
            item.useTurn = true;
            item.useAnimation = 14;
            item.useStyle = 1;
            item.knockBack = 6.5f;
            item.value = Item.buyPrice(1, 50, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SilvestreBlade");
            item.shootSpeed = 18f;

            item.GetGlobalItem<EATooltip>().developer = true;
            item.GetGlobalItem<EARarity>().rare = 12;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silvestre Greatsword");
            Tooltip.SetDefault("The power of creation\nSilvestre's developer weapon");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(null, "NeutronFragment", 8);
            recipe.AddIngredient(null, "VoidLeviathanHeart", 1);
            recipe.AddIngredient(ItemID.Seedler, 1);
            recipe.AddIngredient(ItemID.Starfury, 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
*/