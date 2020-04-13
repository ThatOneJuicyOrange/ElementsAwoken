using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BlazeguardWave2 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 52;
            projectile.height = 52;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 40;
            projectile.light = 1f;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            projectile.GetGlobalProjectile<GlobalProjectiles.EAProjectileType>().specialPenetrate = 1;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazeguard");
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                float alpha = (1-((float)projectile.alpha / 255f)) - ((float)k / (float)projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(252, 32, 3), (float)k / (float)projectile.oldPos.Length) * alpha;
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] > 0) return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.ai[1] > 0 || projectile.timeLeft < 20)
            {
                ProjectileUtils.FadeOut(projectile, 255 / 20);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[1]++;
            target.AddBuff(BuffID.OnFire, 600);
            target.immune[projectile.owner] = 1;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.8f)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }
    }
}