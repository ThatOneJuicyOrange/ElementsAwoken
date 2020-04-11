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
    public class RadiantStarBow : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/RadiantStar"; } }
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;

            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ranged = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 120;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Star");
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                float alpha = (1 - projectile.alpha / 255) - ((float)k / (float)projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(127, 3, 252), (float)k / (float)projectile.oldPos.Length) * alpha;
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation += 0.05f;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 20f)
            {
                ProjectileUtils.Home(projectile, 8f, 800f);
            }
        }
        public override void Kill(int timeLeft)
        {
            float dustAmountScale = ModContent.GetInstance<Config>().lowDust ? 0.3f : 1f;
            ProjectileUtils.OutwardsCircleDust(projectile, DustID.PinkFlame, (int)(24 * dustAmountScale), 9f, randomiseVel: true, dustScale: 2.2f);
        }
    }
}