using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CoilRound : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 300;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coilgun");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.3f, 0.2f, 0.6f);

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (Main.rand.NextBool(4))
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 229)];
                dust.noGravity = true;
                dust.scale = 1f;
                dust.velocity *= 0.1f;
                dust.color = new Color(0,0,255);
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Projectiles/CoilTrail");
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                if (k == 0) continue;
                Vector2 drawPos = projectile.oldPos[k] + new Vector2(projectile.width / 2, projectile.height / 2) - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                sb.Draw(tex, drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 4; k++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 229, projectile.oldVelocity.X * 0.25f, projectile.oldVelocity.Y * 0.25f)];
                dust.color = new Color(0, 0, 255);
            }
        }
    }
}