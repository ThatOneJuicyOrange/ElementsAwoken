using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Manashard
{
    public class ManashardWhip : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 38;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 36;
            item.height = 28;
            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ManashardWhipP");
            item.shootSpeed = 18f;
        }
        public override bool CanUseItem(Player player)
        {
            int maxThrown = 3;
            if (player.ownedProjectileCounts[mod.ProjectileType("ManashardWhipP")] >= maxThrown)
            {
                return false;
            }
            else return true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Manashard Whip");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Manashard", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

