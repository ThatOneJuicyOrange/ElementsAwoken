using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Star : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 1200;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star");
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                for (int i = 0; i < 13; i++)
                {
                    int num1493 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 261, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num1493].noGravity = true;
                    Main.dust[num1493].fadeIn = 1f;
                    Dust dust = Main.dust[num1493];
                    dust.velocity *= 4f;
                    Main.dust[num1493].noLight = true;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                if (Main.rand.Next(10 - (int)Math.Min(7f, projectile.velocity.Length())) < 1)
                {
                    int num1495 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 261, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num1495].noGravity = true;
                    Dust dust = Main.dust[num1495];
                    dust.velocity *= 0.2f;
                    Main.dust[num1495].fadeIn = 0.4f;
                    if (Main.rand.Next(6) == 0)
                    {
                        dust = Main.dust[num1495];
                        dust.velocity *= 5f;
                        Main.dust[num1495].noLight = true;
                    }
                    else
                    {
                        Main.dust[num1495].velocity = projectile.DirectionFrom(Main.dust[num1495].position) * Main.dust[num1495].velocity.Length();
                    }
                }
            }

            Lighting.AddLight(projectile.Center, 0.2f, 0.4f, 0.6f);

            float centerY = projectile.Center.X;
            float centerX = projectile.Center.Y;
            float maxDist = 400f;
            bool home = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                {
                    float num1 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                    float num2 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                    float num3 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num1) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num2);
                    if (num3 < maxDist)
                    {
                        maxDist = num3;
                        centerY = num1;
                        centerX = num2;
                        home = true;
                    }
                }
            }
            if (home)
            {
                float speed = 8f;
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
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector11 = new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] / 2));

            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 vector40 = projectile.Center - Main.screenPosition;
            vector40 -= new Vector2((float)texture.Width, (float)(texture.Height / Main.projFrames[projectile.type])) * projectile.scale / 2f;
            vector40 += vector11 * projectile.scale + new Vector2(0f, projectile.gfxOffY);
            float num147 = 1f / (float)projectile.oldPos.Length * 1.1f;
            int num148 = projectile.oldPos.Length - 1;
            while ((float)num148 >= 0f)
            {
                float num149 = (float)(projectile.oldPos.Length - num148) / (float)projectile.oldPos.Length;
                Color color35 = Color.White;
                color35 *= 1f - num147 * (float)num148 / 1f;
                color35.A = (byte)((float)color35.A * (1f - num149));
                Main.spriteBatch.Draw(texture, vector40 + projectile.oldPos[num148] - projectile.position, null, color35, projectile.oldRot[num148], vector11, projectile.scale * MathHelper.Lerp(0.8f, 0.3f, num149), spriteEffects, 0f);
                num148--;
            }
            texture = Main.extraTexture[57];
            Main.spriteBatch.Draw(texture, vector40, null, Color.White, 0f, texture.Size() / 2f, projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Vector2 spinningpoint = new Vector2(0f, -3f).RotatedByRandom(3.1415927410125732);
            float num71 = 24f;
            Vector2 value = new Vector2(1.05f, 1f);
            float num74;
            for (float num72 = 0f; num72 < num71; num72 = num74 + 1f)
            {
                int num73 = Dust.NewDust(projectile.Center, 0, 0, 66, 0f, 0f, 0, Color.Transparent, 1f);
                Main.dust[num73].position = projectile.Center;
                Main.dust[num73].velocity = spinningpoint.RotatedBy((double)(6.28318548f * num72 / num71), default(Vector2)) * value * (0.8f + Main.rand.NextFloat() * 0.4f) * 2f;
                Main.dust[num73].color = Color.SkyBlue;
                Main.dust[num73].noGravity = true;
                Dust dust = Main.dust[num73];
                dust.scale += 0.5f + Main.rand.NextFloat();
                num74 = num72;
            }
        }
    }
}