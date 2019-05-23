using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.InfinityGauntlet
{
    public class InfiniteSkySwirl : ModProjectile
    {
        public float shrink = 150;
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.timeLeft = 200;
            projectile.extraUpdates = 2;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinite Sky");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 1;
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            if (projectile.localAI[0] > 90)
            {
                if (shrink > 0f)
                {
                    shrink -= 6f;
                }
            }
            else
            {
                shrink += 1f;
            }
            Vector2 offset = new Vector2(shrink, 0);
            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            projectile.ai[0] += 0.05f;
            projectile.Center = parent.Center + offset.RotatedBy(projectile.ai[0] + projectile.ai[1] * (Math.PI * 2 / 8));

            if (shrink <= 0)
            {
                projectile.Kill();
                parent.Kill();
            }
            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 164, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 0.6f;
            Main.dust[dust].noGravity = true;
            int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
            Main.dust[dust2].velocity *= 0.1f;
            Main.dust[dust2].scale *= 0.6f;
            Main.dust[dust2].noGravity = true;
        }

    }
}