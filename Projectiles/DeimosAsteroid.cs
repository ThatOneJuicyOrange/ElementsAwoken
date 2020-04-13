using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DeimosAsteroid : ModProjectile
    {
        private float collideAI = 0;
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = false; 
            projectile.extraUpdates = 1;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deimos");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void AI()
        {
            collideAI++;
            if (collideAI > 30) projectile.tileCollide = true;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            projectile.scale = projectile.ai[1];
            projectile.rotation += projectile.velocity.X * 2f;
            Vector2 position = projectile.Center + Vector2.Normalize(projectile.velocity) * 10f;
            Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 1f)];
            dust.position = position;
            dust.velocity = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * 0.33f + projectile.velocity / 4f;
            dust.position += projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2));
            dust.fadeIn = 0.5f;
            dust.noGravity = true;
            Dust dust1 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 1f)];
            dust1.position = position;
            dust1.velocity = projectile.velocity.RotatedBy(-1.5707963705062866, default(Vector2)) * 0.33f + projectile.velocity / 4f;
            dust1.position += projectile.velocity.RotatedBy(-1.5707963705062866, default(Vector2));
            dust1.fadeIn = 0.5f;
            dust1.noGravity = true;
            for (int num187 = 0; num187 < 1; num187++)
            {
                int num188 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num188].velocity *= 0.5f;
                Main.dust[num188].scale *= 1.3f;
                Main.dust[num188].fadeIn = 1f;
                Main.dust[num188].noGravity = true;
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
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 6, damageType: "magic");
        }
    }
}