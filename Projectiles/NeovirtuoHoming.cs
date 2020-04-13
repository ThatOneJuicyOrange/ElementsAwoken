using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class NeovirtuoHoming : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Neovirtuo");
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, Main.DiscoG, 150);
        }
        public override void AI()
        {
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)
            {
                for (int l = 0; l < 5; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 60)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / 6f * (float)l;
                    dust.noGravity = true;
                    dust.scale = 1f;
                    dust.color = new Color(255, Main.DiscoG, 150);
                }
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            float centerY = projectile.Center.X;
            float centerX = projectile.Center.Y;
            float num = 400f;
            bool home = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                {
                    float num1 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                    float num2 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                    float num3 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num1) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num2);
                    if (num3 < num)
                    {
                        num = num3;
                        centerY = num1;
                        centerX = num2;
                        home = true;
                    }
                }
            }
            if (home)
            {
                float speed = 16f;
                Vector2 vector35 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num4 = centerY - vector35.X;
                float num5 = centerX - vector35.Y;
                float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
                num6 = speed / num6;
                num4 *= num6;
                num5 *= num6;
                projectile.velocity.X = (projectile.velocity.X * 20f + num4) / 21f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + num5) / 21f;
                return;
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 60, damageType: "melee");
        }
    }
}