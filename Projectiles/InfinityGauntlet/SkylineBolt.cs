using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.InfinityGauntlet
{
    public class SkylineBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.scale = 1.0f;
            projectile.width = 12;
            projectile.height = 32;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skyline Bolt");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
            }
            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 164);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
                int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].scale = 1f;
                Main.dust[dust2].velocity *= 0.1f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //if (Main.rand.Next(2) == 0)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("InfiniteSkyBase"), 0, 0, projectile.owner);
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 135, damageType: "magic");
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