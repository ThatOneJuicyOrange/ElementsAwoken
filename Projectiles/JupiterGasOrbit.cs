using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class JupiterGasOrbit : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 100000;
            projectile.penetrate = -1;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jupiter");
        }
        public override void AI()
        {
            int dustType = 31;
            switch (Main.rand.Next(3))
            {
                case 0:
                    dustType = 31;
                    break;
                case 1:
                    dustType = 32;
                    break;
                case 2:
                    dustType = 102;
                    break;
                default: break;
            }
            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 1f);
            Main.dust[dust].velocity *= 0.6f;
            Main.dust[dust].noGravity = true;

            Projectile parent = Main.projectile[(int)projectile.ai[1]];

            projectile.ai[0] += 5f; // speed
            int distance = 75;
            double rad = projectile.ai[0] * (Math.PI / 180); // angle to radians
            float targetX = parent.Center.X - (int)(Math.Cos(rad) * distance);
            float targetY = parent.Center.Y - (int)(Math.Sin(rad) * distance);

            Vector2 toTarget = new Vector2(targetX - projectile.Center.X, targetY - projectile.Center.Y);
            toTarget.Normalize();
            projectile.velocity += toTarget * 0.3f;
            if (Vector2.Distance(projectile.Center, parent.Center) >= 100)
            {
                projectile.velocity *= 0.98f;
            }
            if (!parent.active)
            {
                projectile.Kill();
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //if (!Main.npc[(int)target.ai[3]].boss) // detecting boss worms (if they use ai[3])
            if (target.realLife != -1) // detecting any worm
            {
                target.immune[projectile.owner] = 4;
            }
        }
    }
}