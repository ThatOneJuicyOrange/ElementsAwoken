using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Manaspike : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Manaspike");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 4; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 234)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)num121;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            projectile.velocity.Y += 0.1f;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
            int numberProjectiles = Main.rand.Next(2,5);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(projectile.Center,-projectile.oldVelocity.RotatedByRandom(MathHelper.ToRadians(90)) * 0.5f, ModContent.ProjectileType<Manashatter>(), projectile.damage / 2, 2f, projectile.owner, 0f, 0f);
            }
        }
    }
}