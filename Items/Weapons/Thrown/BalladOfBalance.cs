using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Thrown
{
    public class BalladOfBalance : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 38;  
            item.height = 38;

            item.damage = 85;
            item.knockBack = 4f;

            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.useAnimation = 14;
            item.useTime = 14;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 7;

            item.shoot = mod.ProjectileType("BalladOfBalanceP");
            item.shootSpeed = 16f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("BalladOfBalanceP")] >= 4)
            {
                return false;
            }
            return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ballad Of Balance");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ShadeOfLight", 1);
            recipe.AddIngredient(null, "DanceOfDarkness", 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
