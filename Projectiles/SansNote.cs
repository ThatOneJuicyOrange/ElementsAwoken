using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class SansNote : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 26;

            projectile.friendly = true;
            projectile.magic = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 240;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snote");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.X * 0.1f;
            projectile.spriteDirection = -projectile.direction;
            if (Main.rand.Next(3) == 0)
            {
                int num274 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, 0f, 0f, 80, default(Color), 1f);
                Main.dust[num274].noGravity = true;
                Dust dust = Main.dust[num274];
                dust.velocity *= 0.2f;
            }

            ProjectileGlobal.Home(projectile, 5f);

            for (int k = 0; k < Main.projectile.Length; k++)
            {
                Projectile other = Main.projectile[k];
                if (k != projectile.whoAmI && other.type == projectile.type && other.active && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
                {
                    const float pushAway = 0.01f;
                    if (projectile.position.X < other.position.X)
                    {
                        projectile.velocity.X -= pushAway;
                    }
                    else
                    {
                        projectile.velocity.X += pushAway;
                    }
                    if (projectile.position.Y < other.position.Y)
                    {
                        projectile.velocity.Y -= pushAway;
                    }
                    else
                    {
                        projectile.velocity.Y += pushAway;
                    }
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {

                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 0);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 10;
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