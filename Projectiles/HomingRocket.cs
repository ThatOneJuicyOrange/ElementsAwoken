using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class HomingRocket : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            aiType = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Homing Rocket");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

                projectile.ai[1]++;

            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
                if (!ModContent.GetInstance<Config>().lowDust || projectile.ai[1] % 3 == 0)
                {
                    float num247 = 0f;
                    float num248 = 0f;
                    if (Main.rand.Next(2) == 0)
                    {
                        num247 = projectile.velocity.X * 0.5f;
                        num248 = projectile.velocity.Y * 0.5f;
                    }
                    int num249 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num247, projectile.position.Y + 3f + num248) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 6, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[num249].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                    Main.dust[num249].velocity *= 0.2f;
                    Main.dust[num249].noGravity = true;
                    num249 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num247, projectile.position.Y + 3f + num248) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[num249].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                    Main.dust[num249].velocity *= 0.05f;
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
                float speed = 5f;
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
            ProjectileUtils.Explosion(projectile, 6, damageType: "ranged");
        }
    }
}