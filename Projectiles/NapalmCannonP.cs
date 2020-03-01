using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class NapalmCannonP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;

            projectile.penetrate = -1;

            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = true;

            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Napalm Cannon");
            Main.projFrames[projectile.type] = 2;
        }
        public override void AI()
        {
            if (projectile.ai[1] != 0)
            {
                NPC stick = Main.npc[(int)projectile.ai[0]];
                if (stick.active)
                {
                    projectile.Center = stick.Center - projectile.velocity * 2f;
                    projectile.gfxOffY = stick.gfxOffY;
                }
                else projectile.Kill();
            }
            else
            {
                projectile.velocity.Y += 0.13f;
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                    dust.velocity = Vector2.Zero;
                    dust.noGravity = true;
                    dust.scale = 1f;
               
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] == 0)
            {
                projectile.ai[0] = target.whoAmI;
                projectile.ai[1] = 1;
                projectile.velocity = (target.Center - projectile.Center) * 0.75f;
                projectile.netUpdate = true;
                projectile.frame = 1;
            }
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.frame == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.8f)];
                    dust.noGravity = true;
                    dust.velocity *= 0.5f;
                }
            }
        }
    }
}