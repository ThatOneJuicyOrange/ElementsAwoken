using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.InfinityGauntlet
{
    public class DesertCrystal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.scale = 1.0f;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Crystal");
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
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0.4f) / 255f, ((255 - projectile.alpha) * 1f) / 255f);
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 269);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.2f;

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 269, damageType: "magic");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
    }
}