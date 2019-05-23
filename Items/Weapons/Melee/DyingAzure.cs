using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class DyingAzure : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 28;

            item.damage = 340;
            item.knockBack = 9;

            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
       
            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 30, 0, 0);
            item.GetGlobalItem<EARarity>().rare = 13;
            item.rare = 11;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("DyingAzureP");
            item.shootSpeed = 18f;

        }
        public override bool CanUseItem(Player player)
        {
            int maxThrown = 4;
            if (player.ownedProjectileCounts[mod.ProjectileType("DyingAzureP")] >= maxThrown)
            {
                return false;
            }
            else return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dying Azure");
            Tooltip.SetDefault("Has a chance to annihilate enemies");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DiscordantBar", 15);
            recipe.AddIngredient(null, "ChaoticFlare", 8);
            recipe.AddIngredient(null, "KindleCrusher", 1);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

