using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class MegaRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;

            projectile.scale *= 1.5f;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mega Rocket");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            for (int i = 0; i < 2; i++)
            {
                float num253 = 0f;
                float num254 = 0f;
                if (i == 1)
                {
                    num253 = projectile.velocity.X * 0.5f;
                    num254 = projectile.velocity.Y * 0.5f;
                }
                int num255 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num253, projectile.position.Y + 3f + num254) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 6, 0f, 0f, 100, default(Color), 1f);
                Dust dust = Main.dust[num255];
                dust.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                dust = Main.dust[num255];
                dust.velocity *= 0.2f;
                Main.dust[num255].noGravity = true;
                num255 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num253, projectile.position.Y + 3f + num254) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num255].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                dust = Main.dust[num255];
                dust.velocity *= 0.05f;
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 6, damageType: "ranged");
        }
    }
}