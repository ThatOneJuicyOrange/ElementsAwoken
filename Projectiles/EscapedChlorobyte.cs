using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class EscapedChlorobyte : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chlorobyte");
        }
        public override void AI()
        {
            Player target = Main.player[(int)projectile.ai[0]];

            projectile.localAI[0]++;
            if (projectile.localAI[0] > 45)
            {
                double angle = Math.Atan2(target.Center.Y - projectile.Center.Y, target.Center.X - projectile.Center.X);
                projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 2f;

                if (projectile.Hitbox.Intersects(new Rectangle((int)target.Center.X - 4, (int)target.Center.Y - 4, 8, 8)))
                {
                    projectile.Kill();
                    target.AddBuff(BuffID.Poisoned, 120);
                }
            }
            else
            {
                projectile.velocity *= 0.98f;
            }
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 75)];
                dust.velocity *= 0.6f;
                dust.position -= projectile.velocity / 8f * (float)i;
                dust.noGravity = true;
                dust.scale = 0.8f;
            }    
           if (Vector2.Distance(target.Center,projectile.Center) > 200)
            {
                projectile.Kill();
            }
        }
    }
}
