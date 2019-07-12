using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class HarvesterScythe : ModProjectile
    {
        int healCD = 0;
        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 48;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.melee = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harvester");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 3;
            if (healCD <= 0)
            {
                Main.player[projectile.owner].statLife += 2;
                Main.player[projectile.owner].HealEffect(2);
                healCD = 8;
            }
        }
        public override void AI()
        {
            for (int l = 0; l < 2; l++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 74, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 1f);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= Main.rand.NextFloat(0.5f, 0.9f);
                Main.dust[dust].noGravity = true;
            }

            Lighting.AddLight(projectile.Center, 0.3f, 0.9f, 0.6f);

            projectile.rotation += 0.6f;
            projectile.velocity *= 0.99f;

            projectile.ai[0]++;
            if (projectile.ai[0] >= 45)
            {
                projectile.alpha += 5;
            }
            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }
        }
    }
}