using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class ImmolatorBolt : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.scale = 1.0f;
            projectile.width = 2;
            projectile.height = 2;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 120;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Bolt");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            Lighting.AddLight(projectile.Center, 0.5f, 0.1f, 0f);
            if (Main.rand.Next(2) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 127)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            projectile.localAI[0]++;

            if (projectile.localAI[0] >= 40 && projectile.localAI[0] < 200)
            {
                float speed = 4.5f;
                double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - projectile.position.Y, Main.player[Main.myPlayer].position.X - projectile.position.X);
                projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;

                // how to lag the shit out of terraria: use lots of homing projectiles with this code
                /*float centerY = projectile.Center.X;
                float centerX = projectile.Center.Y;
                float num = 400f;
                bool home = false;
                for (int i = 0; i < 200; i++)
                {
                    if (Collision.CanHit(projectile.Center, 1, 1, Main.player[i].Center, 1, 1))
                    {
                        float num1 = Main.player[i].position.X + (float)(Main.player[i].width / 2);
                        float num2 = Main.player[i].position.Y + (float)(Main.player[i].height / 2);
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
                    float speed = 6f;
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
                }*/
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}