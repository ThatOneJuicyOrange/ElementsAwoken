using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.NPCProj.RadiantMaster
{
    public class RadiantFireball : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.scale *= 0.6f;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Fireball");
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
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.PinkFlame, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
        public override void AI()
        {
            // gravity 
            projectile.velocity.Y = projectile.velocity.Y + 0.05f;
            if (projectile.velocity.Y > 12f)
            {
                projectile.velocity.Y = 12f;
            }
            projectile.rotation = projectile.velocity.ToRotation() - 1.57079637f;
            // dust
            if (!GetInstance<Config>().lowDust)
            {
                if (Main.rand.NextBool(12))
                {
                    int num1489 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num1489].noGravity = true;
                    Main.dust[num1489].fadeIn = 1f;
                    Dust dust = Main.dust[num1489];
                    dust.velocity *= 2f;
                    Main.dust[num1489].noLight = true;
                }
            }
            if (projectile.ai[0] == 0)
            {
                int maxDist = 50;
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Player player = Main.LocalPlayer;
                    if (!player.dead && player.active && Vector2.Distance(player.Center, projectile.Center) < maxDist)
                    {
                        KillWithStasis();
                    }
                }
                else
                {
                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player player = Main.player[i];
                        if (!player.dead && player.active && Vector2.Distance(player.Center, projectile.Center) < maxDist)
                        {
                            KillWithStasis();
                        }
                    }
                }
            }
        }
        private void KillWithStasis()
        {
            projectile.Kill();
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ProjectileType<RadiantStasisField>(), 0, 0f, Main.myPlayer);
        }
    }
}