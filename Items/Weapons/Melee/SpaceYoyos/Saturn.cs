using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee.SpaceYoyos
{
    public class Saturn : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;

            item.useStyle = 5;
            item.damage = 92;
            item.knockBack = 1f;

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.rare = 7;
            item.value = Item.sellPrice(0, 15, 0, 0);

            item.useAnimation = 25;
            item.useTime = 25;
            item.UseSound = SoundID.Item1;

            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("SaturnP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saturn");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Neptune", 1);
            recipe.AddIngredient(null, "MysticLeaf", 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
