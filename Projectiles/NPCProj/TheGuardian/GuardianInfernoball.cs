using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianInfernoball : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.scale = 1.0f;
            projectile.width = 68;
            projectile.height = 68;
            projectile.penetrate = 1;

            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            projectile.alpha = 0;
            projectile.timeLeft = 600;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
            Main.projFrames[projectile.type] = 4;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.2f, 0.4f);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (Main.rand.NextBool(6))
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dust].velocity *= 0.3f;
                Main.dust[dust].fadeIn = 0.9f;
                Main.dust[dust].noGravity = true;
            }
            if (Vector2.Distance(Main.player[Main.myPlayer].Center, projectile.Center) <= 30)
            {
                projectile.Kill();
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
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
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 200);
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.HostileExplosion(projectile, 6);
        }
    }
}