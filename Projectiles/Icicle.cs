using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Icicle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            aiType = ProjectileID.Bullet;

            projectile.friendly = true;
            projectile.magic = true;

            projectile.penetrate = 2;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Spike");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 30, false);
            projectile.scale *= 0.7f;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.4f) / 255f, ((255 - projectile.alpha) * 0.2f) / 255f, ((255 - projectile.alpha) * 1f) / 255f);
            if (Main.rand.NextBool(3))
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 0.8f;
                Main.dust[dust].velocity *= 0.1f;
            }
        }

    }
}