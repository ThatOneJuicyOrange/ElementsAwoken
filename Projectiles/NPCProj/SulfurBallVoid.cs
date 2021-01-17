using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class SulfurBallVoid : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;

            projectile.hostile = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 300;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Concerntrated Void Ball");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);

            projectile.velocity.Y += projectile.ai[0];
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.Kill();
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Projectiles/NPCProj/SulfurBallVoidTrail");
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                float alpha = 1 - ((float)k / (float)projectile.oldPos.Length);
                float scale = 1.1f - ((float)k / (float)projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(153, 41, 145), (float)k / (float)projectile.oldPos.Length) * alpha;
                sb.Draw(tex, drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 103, 1, -0.5f);
            for (int k = 0; k < 12; k++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.PinkFlame, projectile.oldVelocity.X * 0.6f, projectile.oldVelocity.Y * 0.6f)];
                dust.scale *= 2.6f;
                dust.noGravity = true;
            }
        }
    }
}