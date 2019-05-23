using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.CosmicObserver
{
    public class EnergyFork : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;

            item.damage = 49;
            item.knockBack = 3f;

            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = false;

            item.useAnimation = 16;
            item.useStyle = 1;
            item.useTime = 16;
            item.UseSound = SoundID.Item39;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;

            item.shoot = mod.ProjectileType("EnergyFork");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Fork");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CosmicShard", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
