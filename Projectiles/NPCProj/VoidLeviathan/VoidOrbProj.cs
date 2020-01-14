using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.VoidLeviathan
{
    public class VoidOrbProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
            projectile.scale = 0.6f;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Orb");
            Main.projFrames[projectile.type] = 2;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.2f, 0.5f);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (!ModContent.GetInstance<Config>().lowDust && Main.rand.Next(2) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            Player P = Main.player[Main.myPlayer];
            projectile.ai[0]++;
            if (projectile.ai[0] >= 60)
            {
                float speed = 3f;
                float toX = P.Center.X - projectile.Center.X;
                float toY = P.Center.Y - projectile.Center.Y;
                float num6 = (float)Math.Sqrt((double)(toX * toX + toY * toY));
                num6 = speed / num6;
                toX *= num6;
                toY *= num6;
                projectile.velocity.X = (projectile.velocity.X * 20f + toX) / 21f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + toY) / 21f;
            }
            if (projectile.ai[0] > 180)
            {
                projectile.alpha += 255 / 30;
                if (projectile.alpha >= 255) projectile.Kill();
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
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
            return true;
        }
    }
}