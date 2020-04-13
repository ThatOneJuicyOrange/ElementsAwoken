using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class RadiantBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 900;
            projectile.penetrate = -1;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Blade");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Starstruck>(), 300);
            target.immune[projectile.owner] = 5;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            ProjectileUtils.PushOtherEntities(projectile);

            if (!ModContent.GetInstance<Config>().lowDust)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame)];
                dust.velocity *= 0.1f;
                dust.scale *= 0.2f;
                dust.noGravity = true;
                dust.fadeIn = 1f;
            }
            Vector2 targetPos = projectile.position;
            float targetDist = 700;
            bool target = false;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.CanBeChasedBy(this, false))
                {
                    float distance = Vector2.Distance(npc.Center, projectile.Center);
                    if (distance < targetDist)
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        target = true;
                    }
                }
            }
            if (target)
            {
                Vector2 toTarget = targetPos - projectile.Center;
                float dist = (float)Math.Sqrt(toTarget.X * toTarget.X + toTarget.Y * toTarget.Y);
                toTarget.Normalize();
                if (dist >= 150)
                {
                    projectile.velocity = toTarget * 18;
                }
            }
            else
            {
                float speed = 16f;
                Vector2 toTarget = player.Center - projectile.Center;
                float dist = (float)Math.Sqrt(toTarget.X * toTarget.X + toTarget.Y * toTarget.Y);
                if (dist < 100)
                {
                    //projectile.velocity *= 0.97f;
                }
                else if (dist < 2000)
                {
                    dist = speed / dist;
                    toTarget.X *= dist;
                    toTarget.Y *= dist;
                    projectile.velocity.X = (projectile.velocity.X * 20f + toTarget.X) / 21f;
                    projectile.velocity.Y = (projectile.velocity.Y * 20f + toTarget.Y) / 21f;
                }
                else
                {
                    projectile.Center = player.Center;
                }
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
            for (int i = 0; i < 16; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.2f)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
                dust.fadeIn = 0.9f;
            }
        }
    }
}