using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class ExtinctionArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;

            projectile.arrow = true;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.timeLeft = 600;
            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Extinction Arrow");
        }
        public override void AI()
        {
            Lighting.AddLight((int)projectile.Center.X, (int)projectile.Center.Y, 0.6f, 0.1f, 0.5f);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 5f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ExtinctionCurse"), 200);
            target.immune[projectile.owner] = 9;
        }
    }
}