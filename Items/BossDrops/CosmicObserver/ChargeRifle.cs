using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.CosmicObserver
{
    public class ChargeRifle : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 34;

            item.damage = 40;
            item.knockBack = 3.75f;

            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.UseSound = SoundID.Item15;

            item.noMelee = true;
            item.autoReuse = false;
            item.ranged = true;
            item.channel = true;
            item.noUseGraphic = true;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;

            item.shoot = ModContent.ProjectileType<ChargeRifleHeld>();
            item.shootSpeed = 20f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<ChargeRifleHeld>(), damage, knockBack, player.whoAmI, 0.0f, type);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charge Rifle");
            Tooltip.SetDefault("Hold Left Click to charge");
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
