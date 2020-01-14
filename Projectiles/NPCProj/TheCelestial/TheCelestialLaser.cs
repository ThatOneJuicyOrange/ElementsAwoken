using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheCelestial
{
    public class TheCelestialLaser : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.hostile = true;

            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            projectile.light = 2f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Celestial");
            Main.projFrames[projectile.type] = 4;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.3f, 0.3f, 0.3f);

            if (ModContent.GetInstance<Config>().lowDust)
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                projectile.scale = 0.4f;
                projectile.alpha = 0; 
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    int dustType = 6;
                    switch ((int)projectile.ai[0])
                    {
                        case 0:
                            dustType = 6;
                            break;
                        case 1:
                            dustType = 197;
                            break;
                        case 2:
                            dustType = 229;
                            break;
                        case 3:
                            dustType = 242;
                            break;
                        default: break;
                    }
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / 6f * (float)i;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = (int)projectile.ai[0];
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