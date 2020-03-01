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
    public class KindleFireball : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;

            projectile.friendly = true;
            projectile.melee = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kindle Crusher");
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.99f;
            projectile.velocity.Y *= 0.99f;

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            projectile.ai[0]++;

            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            if (!ModContent.GetInstance<Config>().lowDust && projectile.ai[0] % 2 == 0)
            {
                int dustWidth = 4;
                Dust dust = Main.dust[Dust.NewDust(projectile.Center - new Vector2(dustWidth / 2, dustWidth / 2), dustWidth, dustWidth, 6)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f;
                dust.noGravity = true;
            }

            ProjectileGlobal.Home(projectile, 5f);
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