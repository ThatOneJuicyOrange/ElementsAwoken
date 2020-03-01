using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheCelestial
{
    public class CelestialStarShot : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;

            projectile.hostile = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.light = 2f;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Celestials");
        }
        public override void AI()
        {
            projectile.rotation += 0.1f;
            Lighting.AddLight(projectile.Center, 0.0f, 0.2f, 0.4f);
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                float numDusts = 3;
                for (int i = 0; i < numDusts; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center - Vector2.One * 2, 4, 4, 15)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / numDusts * (float)i;
                    dust.noGravity = true;
                    dust.scale = 0.7f;
                }
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                float alpha = 1 - ((float)k / (float)projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, lightColor * alpha, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}