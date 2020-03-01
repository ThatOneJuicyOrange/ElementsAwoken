using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianPortalSwirl : ModProjectile
    {
        public bool shrinking = true;
        public float aiTimer = 100;
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 1000;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.alpha >= 60) return false;
            return base.CanHitPlayer(target);
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.2f, 0.4f);
            Projectile parent = Main.projectile[(int)projectile.ai[1]];
            if (!parent.active) projectile.Kill();
            aiTimer++;
            float distance = 50 * (float)(1 + Math.Sin(aiTimer / 10f));
            double rad = projectile.ai[0] * (Math.PI / 180);
            projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
            projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;

            projectile.alpha = parent.alpha;
            if (Main.rand.NextBool(6))
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 2.75f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
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