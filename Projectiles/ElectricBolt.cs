using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ElectricBolt : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = 6;
            projectile.alpha = 255;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Little Buddy");
        }
        public override void AI()
        {
            if (projectile.localAI[1] == 0)
            {
                projectile.ai[0] = projectile.damage;
                projectile.localAI[1]++;
            }
            projectile.localAI[0]++;
            if (projectile.localAI[0] > 3)
            {
                float numDust = 8;
                for (int l = 0; l < numDust; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 229)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / numDust * (float)l;
                    dust.noGravity = true;
                    dust.scale = 1.3f;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.penetrate--; // so it bounces 6 times but can only hit 3 enemies
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                projectile.damage += (int)(projectile.ai[0] * 0.5f);
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                projectile.velocity *= 1.5f;
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 4; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 229, projectile.oldVelocity.X * 0.25f, projectile.oldVelocity.Y * 0.25f);
            }
        }
    }
}