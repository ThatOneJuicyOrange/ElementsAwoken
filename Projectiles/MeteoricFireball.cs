using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class MeteoricFireball : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.friendly = true;
            projectile.magic = true;

            projectile.timeLeft = 600;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteoric Fireball");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.2f, 0.4f);

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (Main.rand.NextBool(5))
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 1.5f;
                Main.dust[dust].noGravity = true;
            }
            Move(0.2f, Main.MouseWorld);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                float alpha = 1 - ((float)k / (float)projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(252, 32, 3), (float)k / (float)projectile.oldPos.Length) * alpha;
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        private void Move(float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - projectile.Center;

            if (projectile.velocity.X < desiredVelocity.X)
            {
                projectile.velocity.X = projectile.velocity.X + speed;
                if (projectile.velocity.X < 0f && desiredVelocity.X > 0f)
                {
                    projectile.velocity.X = projectile.velocity.X + speed;
                }
            }
            else if (projectile.velocity.X > desiredVelocity.X)
            {
                projectile.velocity.X = projectile.velocity.X - speed;
                if (projectile.velocity.X > 0f && desiredVelocity.X < 0f)
                {
                    projectile.velocity.X = projectile.velocity.X - speed;
                }
            }
            if (projectile.velocity.Y < desiredVelocity.Y)
            {
                projectile.velocity.Y = projectile.velocity.Y + speed;
                if (projectile.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y + speed;
                    return;
                }
            }
            else if (projectile.velocity.Y > desiredVelocity.Y)
            {
                projectile.velocity.Y = projectile.velocity.Y - speed;
                if (projectile.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - speed;
                    return;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileGlobal.Explosion(projectile, new int[] { 6 }, projectile.damage,"magic");
        }
    }
}