using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class VoidSinewave : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.alpha = 255;
            projectile.timeLeft = 600;

            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.ranged = true;

            projectile.penetrate = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade of the Night");
        }
        public override void AI()
        {
            int dustLength = 7;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
                dust.scale *= 0.7f;
            }
            projectile.localAI[0] += 0.075f;
            if (projectile.localAI[0] > 8f)
            {
                projectile.localAI[0] = 8f;
            }
            float rotateIntensity = projectile.localAI[0];
            float waveTime = 16f;
            projectile.ai[0]++;
            if (projectile.ai[1] == 0) // this part is to fix the offset (it is still slightlyyyy offset)
            {
                if (projectile.ai[0] > waveTime * 0.5f)
                {
                    projectile.ai[0] = 0;
                    projectile.ai[1] = 1;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
            }
            else
            {
                if (projectile.ai[0] <= waveTime)
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    projectile.velocity = perturbedSpeed;
                }
                if (projectile.ai[0] >= waveTime * 2)
                {
                    projectile.ai[0] = 0;
                }
            }
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
                float speed = 25f;
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
    }
}