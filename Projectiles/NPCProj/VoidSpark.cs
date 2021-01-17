using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class VoidSpark : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidbroken Spark");
        }
        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector11 = new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] / 2));
            Color color9 = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
            float num66 = 0f;
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 vector39 = projectile.Center - Main.screenPosition;

            vector39 -= new Vector2((float)texture.Width, (float)(texture.Height / Main.projFrames[projectile.type])) * projectile.scale / 2f;
            vector39 += vector11 * projectile.scale + new Vector2(0f, num66 + projectile.gfxOffY);
            texture = Main.projectileTexture[projectile.type];
            Rectangle frame = new Rectangle(0, texture.Height * projectile.frame, texture.Width, texture.Height);
            Main.spriteBatch.Draw(texture, vector39, frame, projectile.GetAlpha(color9), projectile.rotation, vector11, projectile.scale, spriteEffects, 0f);
            float num143 = 1f / (float)projectile.oldPos.Length * 0.7f;
            int num144 = projectile.oldPos.Length - 1;
            while (num144 >= 0f)
            {
                float num145 = (float)(projectile.oldPos.Length - num144) / (float)projectile.oldPos.Length;
                Color color34 = Color.Pink;
                color34 *= 1f - num143 * (float)num144 / 1f;
                color34.A = (byte)((float)color34.A * (1f - num145));
                Main.spriteBatch.Draw(texture, vector39 + projectile.oldPos[num144] - projectile.position, new Rectangle?(), color34, projectile.oldRot[num144], vector11, projectile.scale * MathHelper.Lerp(0.3f, 1.1f, num145), spriteEffects, 0f);
                num144--;
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Rectangle hitbox2 = projectile.Hitbox;
            for (int num67 = 0; num67 < projectile.oldPos.Length; num67 += 3)
            {
                hitbox2.X = (int)projectile.oldPos[num67].X;

                hitbox2.Y = (int)projectile.oldPos[num67].Y;
                int dustAmount = 3;
                if (ModContent.GetInstance<Config>().lowDust) dustAmount = 1;
                for (int i = 0; i < dustAmount; i++) // normally 5
                {
                    int num70 = Dust.NewDust(hitbox2.TopLeft(), projectile.width, projectile.height, DustID.PinkFlame, 2.5f, -2.5f, 0, default(Color), 1f);
                    Main.dust[num70].alpha = 200;
                    Dust dust = Main.dust[num70];
                    dust.velocity *= 1f; // normally 2.4 but dusts go everywhere
                    dust = Main.dust[num70];
                    dust.scale += Main.rand.NextFloat();
                }
            }
        }
        public override void AI()
        {
           
            // gravity 
            projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            if (projectile.velocity.Y > 12f)
            {
                projectile.velocity.Y = 12f;
            }
            projectile.rotation += 0.02f;
            // dust
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f; 
                int dustAmount = 3;
                if (ModContent.GetInstance<Config>().lowDust) dustAmount = 1;
                for (int i = 0; i < dustAmount; i++)// normally 13 but wtf vanilla
                {
                    int num1489 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num1489].noGravity = true;
                    Main.dust[num1489].fadeIn = 1f;
                    Dust dust = Main.dust[num1489];
                    dust.velocity *= 2f;// normally 4 but wtf vanilla
                    Main.dust[num1489].noLight = true;
                }
            }
            if (!ModContent.GetInstance<Config>().lowDust) for (int i = 0; i < 3; i++) CreateDust();
            else if (Main.rand.Next(3) == 0) CreateDust();
            return;
        }
        private void CreateDust()
        {
            if (projectile.ai[0] == 0)
            {
                int num1491 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                Main.dust[num1491].noGravity = true;
                Dust dust = Main.dust[num1491];
                dust.velocity *= 0.2f;
                Main.dust[num1491].fadeIn = 1f;
                if (Main.rand.Next(6) == 0)
                {
                    dust = Main.dust[num1491];
                    dust.velocity *= 10f; // normally 30 but wtf vanilla
                    Main.dust[num1491].noGravity = false;
                    Main.dust[num1491].noLight = true;
                }
                else
                {
                    Main.dust[num1491].velocity = projectile.DirectionFrom(Main.dust[num1491].position) * Main.dust[num1491].velocity.Length();
                }
            }
        }
    }
}