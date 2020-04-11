using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class PutridArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.timeLeft = 600;

            projectile.arrow = true;
            projectile.friendly = true;
            projectile.ranged = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Putrid Arrow");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.velocity.Y += 0.06f;
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 46, 0f, 0f, 150, default(Color), 1f)];
                dust.noGravity = true;
            }
            if (projectile.ai[0] > 0)
            {
                projectile.velocity.Y += projectile.ai[0];
            }
        }
        public override void Kill(int timeLeft)
        {
            if (Main.rand.NextBool(3))
            {
                for (int i = 0; i < Main.rand.Next(1,4); i++)
                {
                    Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedByRandom(MathHelper.ToRadians(6)) * 0.5f, ProjectileType<PutridGoop>(), (int)(projectile.damage * 0.75f), 0, projectile.owner);
                }
            }
        }
    }
}