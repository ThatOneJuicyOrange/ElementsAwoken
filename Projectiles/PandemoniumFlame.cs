using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class PandemoniumFlame : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = true;

            projectile.alpha = 255;
            projectile.penetrate = 1;

            projectile.timeLeft = 300; 
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pandemonium");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f); 
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                for (int l = 0; l < 2; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 127)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / 6f * (float)l;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
            else
            {
                projectile.alpha = 0;
                projectile.scale = 0.6f;
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            }
            float targetX = projectile.Center.X;
            float targetY = projectile.Center.Y;
            float closestEntity = 400f;
            bool home = false;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    float dist = Math.Abs(projectile.Center.X - nPC.Center.X) + Math.Abs(projectile.Center.Y - nPC.Center.Y);
                    if (dist < closestEntity)
                    {
                        closestEntity = dist;
                        targetX = nPC.Center.X;
                        targetY = nPC.Center.Y;
                        home = true;
                    }
                }
            }
            if (home)
            {
                float speed = 5.5f;
                float goToX = targetX - projectile.Center.X;
                float goToY = targetY - projectile.Center.Y;
                float dist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                dist = speed / dist;
                goToX *= dist;
                goToY *= dist;
                projectile.velocity.X = (projectile.velocity.X * 20f + goToX) / 21f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + goToY) / 21f;
                return;
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (ModContent.GetInstance<Config>().lowDust)
            {
                Texture2D tex = Main.projectileTexture[projectile.type];
                Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, projectile.height * 0.5f);
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    Rectangle rectangle = new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]);
                    sb.Draw(tex, drawPos, rectangle, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
    }
}
