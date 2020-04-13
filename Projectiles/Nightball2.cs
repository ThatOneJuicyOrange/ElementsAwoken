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
    public class Nightball2 : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/LaserTex"; } }
        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;

            projectile.magic = true;
            projectile.friendly = true;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathwarp");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            Lighting.AddLight((int)projectile.Center.X, (int)projectile.Center.Y, 0f, 0.1f, 0.5f);
            float trailLength = 20f;
            if (projectile.ai[1] == 0f)
            {
                projectile.localAI[0] += 3f;
                if (projectile.localAI[0] > trailLength)
                {
                    projectile.localAI[0] = trailLength;
                }
            }
            else
            {
                projectile.localAI[0] -= 3f;
                if (projectile.localAI[0] <= 0f)
                {
                    projectile.Kill();
                    return;
                }
            }
            ProjectileUtils.Home(projectile, 6f, 600);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(211, 28, 214, 0);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Color color32 = projectile.GetAlpha(lightColor);
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            int trailNum = 0;
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                for (int i = 0; i < 2; i++) // to draw between the positions
                {
                    trailNum++;
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    if (k < projectile.oldPos.Length - 1) drawPos = (projectile.oldPos[k] + projectile.oldPos[k + i]) / 2 - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    float alpha = 1 - ((float)trailNum / (float)(projectile.oldPos.Length * 2));
                    float scale = 1 - ((float)trailNum / (float)(projectile.oldPos.Length * 2));
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color32, projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
                }
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 173, damageType: "melee");
        }
    }
}