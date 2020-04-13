using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FrostPulse : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Pulse");
        }
        public override void AI()
        {
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 15;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 135)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)num121;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 135, damageType: "ranged");
        }
    }
}