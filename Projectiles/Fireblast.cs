using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Fireblast : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireblast");
            Main.projFrames[projectile.type] = 4;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
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
            /*for (int k = 0; k < 2; k++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 3f;
                Main.dust[dust].noGravity = true;
            }*/
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] == 12f)
            {
                projectile.localAI[0] = 0f;
                for (int l = 0; l < 12; l++)
                {
                    Vector2 vector3 = Vector2.UnitX * (float)(-(float)projectile.width) / 2f;
                    vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
                    vector3 = vector3.RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2));
                    int num9 = Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
                    Main.dust[num9].scale = 1.1f;
                    Main.dust[num9].noGravity = true;
                    Main.dust[num9].position = projectile.Center + vector3;
                    Main.dust[num9].velocity = projectile.velocity * 0.1f;
                    Main.dust[num9].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num9].position) * 1.25f;
                }
            }
            if (Main.rand.Next(4) == 0)
            {
                for (int m = 0; m < 1; m++)
                {
                    Vector2 value = -Vector2.UnitX.RotatedByRandom(0.19634954631328583).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num10 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[num10].velocity *= 0.1f;
                    Main.dust[num10].position = projectile.Center + value * (float)projectile.width / 2f;
                    Main.dust[num10].fadeIn = 0.9f;
                }
            }
            if (Main.rand.Next(32) == 0)
            {
                for (int n = 0; n < 1; n++)
                {
                    Vector2 value2 = -Vector2.UnitX.RotatedByRandom(0.39269909262657166).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num11 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 155, default(Color), 0.8f);
                    Main.dust[num11].velocity *= 0.3f;
                    Main.dust[num11].position = projectile.Center + value2 * (float)projectile.width / 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num11].fadeIn = 1.4f;
                    }
                }
            }
            if (Main.rand.Next(2) == 0)
            {
                for (int num12 = 0; num12 < 2; num12++)
                {
                    Vector2 value3 = -Vector2.UnitX.RotatedByRandom(0.78539818525314331).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num13 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 1.2f);
                    Main.dust[num13].velocity *= 0.3f;
                    Main.dust[num13].noGravity = true;
                    Main.dust[num13].position = projectile.Center + value3 * (float)projectile.width / 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num13].fadeIn = 1.4f;
                    }
                }
            }



            Lighting.AddLight(projectile.Center,0.9f, 0.2f, 0.2f);
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
                float speed = 12f;
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
            ProjectileUtils.Explosion(projectile, 6, damageType: "magic");
        }
    }
}