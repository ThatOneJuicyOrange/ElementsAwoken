using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class EnergyFork : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;

            projectile.thrown = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Fork");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.2f, 0.8f, 0.3f);

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 220);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 1.5f;
                Main.dust[dust].noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 220, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int dir = Main.rand.Next(2) == 0 ? -1 : 1;
            Projectile.NewProjectile(target.Center.X, target.Center.Y - 140 * dir, 0f, 12 * dir, mod.ProjectileType("EnergyFork2"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }
    }
}