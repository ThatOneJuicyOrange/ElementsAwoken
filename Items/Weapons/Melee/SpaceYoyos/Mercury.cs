using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee.SpaceYoyos
{
    public class Mercury : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;

            item.damage = 24;
            item.knockBack = 5f;

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.rare = 2;
            item.value = Item.sellPrice(0, 4, 0, 0);

            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.UseSound = SoundID.Item1;

            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("MercuryP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mercury");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Mars", 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
