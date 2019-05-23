using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class FrayedFeather : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;

            projectile.penetrate = 1;
            projectile.timeLeft = 600;

            projectile.thrown = true;
            projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Knife");
        }
        public override void AI()
        {
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 60f)
            {
                projectile.alpha += 5;
                projectile.damage = (int)((double)projectile.damage * 0.95);
                projectile.knockBack = (float)((int)((double)projectile.knockBack * 0.95));
                projectile.rotation -= 0.3f;
            }
            if (projectile.localAI[0] < 60f)
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            }
            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }
            int dust = Dust.NewDust(projectile.Center - new Vector2(projectile.width / 2, projectile.height / 2), projectile.width / 2, projectile.height/ 2, 21);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 21, projectile.oldVelocity.X * 0.25f, projectile.oldVelocity.Y * 0.25f);
            }
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 120);
        }
    }
}