using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class SnowdriftBomb : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.friendly = true;
            projectile.magic = true;

            projectile.penetrate = -1;
            projectile.extraUpdates = 1;

            projectile.timeLeft = 200;
            projectile.scale = 0.9f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowdrift");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.05f) / 255f, ((255 - projectile.alpha) * 0.25f) / 255f, ((255 - projectile.alpha) * 0.5f) / 255f);

            projectile.velocity.Y += 0.1f;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            for (int l = 0; l < 5; l++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 135)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)l;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 135, damageType: "magic");
        }
    }
}