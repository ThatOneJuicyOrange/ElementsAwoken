using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Worm : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.friendly = true;
            projectile.melee = true;

            projectile.penetrate = -1;

            projectile.timeLeft = 500;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm");
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
                projectile.velocity.Y += 0.05f;
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] == 0)
            {
                if (projectile.timeLeft > 300) projectile.timeLeft = 300;

                projectile.ai[0] = target.whoAmI;
                projectile.ai[1] = 1;
                projectile.velocity = (target.Center - projectile.Center) * 0.75f;
                projectile.netUpdate = true;
            }
            target.immune[projectile.owner] = 20;

        }

    }
}