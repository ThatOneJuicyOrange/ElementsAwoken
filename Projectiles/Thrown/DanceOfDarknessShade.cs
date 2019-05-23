using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class DanceOfDarknessShade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.thrown = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;

            projectile.timeLeft = 200;
            projectile.penetrate = -1;
            projectile.alpha = 50;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dance Of Darkness");
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(61, 49, 91, projectile.alpha);
        }
        public override void AI()
        {
            projectile.rotation += 0.5f;

            projectile.localAI[0]++;
            if (projectile.localAI[0] > 60)
            {
                projectile.ai[0]++;
            }
            if (projectile.ai[0] != 0)
            {
                projectile.alpha += 5;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0]++;
        }
    }
}