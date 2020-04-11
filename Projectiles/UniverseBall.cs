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
    class UniverseBall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;

            projectile.friendly = true;
            projectile.melee = true;

            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Universe Ball");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            ProjectileUtils.Home(projectile, 5f);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            Texture2D outlineTex = mod.GetTexture("Projectiles/UniverseBall1");
            int trailNum = 0;
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                for (int i = 0; i < 2; i++) // to draw between the positions
                {
                    trailNum++;
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    if (k < projectile.oldPos.Length - 1) drawPos = (projectile.oldPos[k] + projectile.oldPos[k + i]) / 2 - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    float alpha = 1 - ((float)trailNum / (float)(projectile.oldPos.Length * 2));
                    Color color = Color.Lerp(Color.White, new Color(85, 44, 156), (float)trailNum / (float)(projectile.oldPos.Length*2)) * alpha;
                    float scale = 1 - ((float)trailNum / (float)(projectile.oldPos.Length * 2));
                    sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
    }
}