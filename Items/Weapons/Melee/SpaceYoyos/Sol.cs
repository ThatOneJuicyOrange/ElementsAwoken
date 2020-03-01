using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee.SpaceYoyos
{
    public class Sol : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;

            item.useStyle = 5;
            item.damage = 390;
            item.knockBack = 8f;

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.rare = 0;
            item.value = Item.sellPrice(0, 50, 0, 0);
            item.GetGlobalItem<EARarity>().rare = 13;

            item.useAnimation = 25;
            item.useTime = 25;
            item.UseSound = SoundID.Item1;

            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("SolP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sol");
            Tooltip.SetDefault("Has a strong gravitational pull");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Jupiter", 1);
            recipe.AddIngredient(null, "DiscordantBar", 15);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
