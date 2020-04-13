using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BlazeguardWave : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 86;
            projectile.height = 86;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.light = 1f;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
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
                float alpha = (1 - ((float)projectile.alpha / 255f)) - ((float)k / (float)projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(252, 32, 3), (float)k / (float)projectile.oldPos.Length) * alpha;
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            int hitboxSize = 16;
            if (Collision.SolidCollision(projectile.Center - new Vector2(hitboxSize / 2, hitboxSize / 2), hitboxSize, hitboxSize)) projectile.ai[1] = 2;

            if (projectile.ai[1] > 0 || projectile.timeLeft < 20)
            {
                int amount = 20;
                if (projectile.ai[1] == 2) amount = 10;
                ProjectileUtils.FadeOut(projectile, 255 / amount);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] > 0) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[1]++;
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14,1,-0.5f);
            int numberProjectiles = Main.rand.Next(2, 5);
            float max = target.height * 3f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile wave = Main.projectile[Projectile.NewProjectile(target.Center + Main.rand.NextVector2Square(-max, max),Vector2.Zero, ModContent.ProjectileType<BlazeguardWave2>(), projectile.damage / 2, 0, projectile.owner)];
                Vector2 toTarget = target.Center - wave.Center;
                toTarget.Normalize();
                wave.velocity = toTarget * 6f;
            }
        }

        public override void Kill(int timeLeft)
        {

        }
    }
}