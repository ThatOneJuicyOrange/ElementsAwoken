using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheCelestial
{
    public class Solus : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 60;

            item.damage = 70;
            item.knockBack = 18;

            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.shoot = mod.ProjectileType("SolusP");
            item.shootSpeed = 5f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solus");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(7);
            float speed = 5f;
            float xSpeed = player.direction == 1 ? speed : -speed;
            position += Vector2.Normalize(new Vector2(xSpeed, 3f)) * 10f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(position.X, position.Y, xSpeed, 3f, type, damage, knockBack, player.whoAmI, -(i * 2));
            }
            return false;
        }
    }
}
