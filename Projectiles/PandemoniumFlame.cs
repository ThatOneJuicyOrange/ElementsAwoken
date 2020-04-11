using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.GlobalProjectiles;
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
            projectile.width = 12;
            projectile.height = 12;

            projectile.friendly = true;

            projectile.alpha = 0;
            projectile.scale = 0.6f;
            projectile.penetrate = 1;

            projectile.timeLeft = 300; 
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pandemonium");
            Main.projFrames[projectile.type] = 2;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            int num = ModContent.GetInstance<Config>().lowDust ? 9 : 4;

            projectile.ai[0]++;
            if (projectile.ai[0] % num == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, Main.rand.Next(4) == 0 ? 31: 127, 0f, 0f, 100, default(Color), 0.5f)];
                dust.fadeIn = 1f + Main.rand.NextFloat(-0.5f,0.5f);
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
            ProjectileUtils.Home(projectile, 6f);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle rect = new Rectangle(0, 4, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height - 4);
                if (k == 0) rect.Y -= 4;
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color, projectile.rotation, drawOrigin, scale * projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
