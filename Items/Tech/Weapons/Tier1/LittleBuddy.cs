using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier1
{
    public class LittleBuddy : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 26;
            
            item.damage = 9;
            item.knockBack = 3.5f;

            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 5;

            item.GetGlobalItem<ItemEnergy>().energy = 2;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 0, 10, 0);
            item.rare = 2;

            item.UseSound = SoundID.Item92;
            item.shootSpeed = 4f;
            item.shoot = mod.ProjectileType("ElectricBolt");
            item.useAmmo = 97;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Little Buddy");
            Tooltip.SetDefault("Fires a blue bolt that gains speed and damage each time it bounces");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("ElectricBolt"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(null, "CopperWire", 10);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
