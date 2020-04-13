using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class NecrosisP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 75;
            projectile.thrown = true;
            projectile.aiStyle = 27;
            projectile.timeLeft = 60;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necrosis");
        }
        public override void AI()
        {
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 240f)
            {
                projectile.alpha += 3;
                projectile.damage = (int)((double)projectile.damage * 0.95);
                projectile.knockBack = (float)((int)((double)projectile.knockBack * 0.95));
            }
            if (projectile.ai[0] < 240f)
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 0.785f;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 62);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 62, damageType: "thrown");
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 300);
        }
    }
}