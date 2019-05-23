using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.CosmicObserver
{
    public class Demolecularizer : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 44;

            item.damage = 42;
            item.knockBack = 2f;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;

            item.shoot = 10;
            item.shootSpeed = 8f;
            item.useAmmo = 40;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demolecularizer");
            Tooltip.SetDefault("Turns normal arrows into cosmic arrows\nHas a chance to fire a cosmic hyperbeam");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.rand.Next(6) == 0)
            {
                type = mod.ProjectileType("DemolecularizerLaser");
                speedX *= 2f;
                speedY *= 2f;
                damage = (int)(damage * 1.5f);
            }
            else
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = mod.ProjectileType("CosmicArrow");
                    speedX *= 1.5f;
                    speedY *= 1.5f;
                }
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
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
