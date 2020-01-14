using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class TriswordBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;

            projectile.friendly = true;
            projectile.melee = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 300;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Trisword");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.alpha != 0) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0]++;
            Vector2 slashPos =  new Vector2(1, 1);
            switch (Main.rand.Next(4))
            {
                case 0:
                    slashPos =new Vector2(1, 1);
                    break;
                case 1:
                    slashPos = new Vector2(1, -1);
                    break;
                case 2:
                    slashPos = new Vector2(-1, -1);
                    break;
                case 3:
                    slashPos =  new Vector2(-1, 1);
                    break;
                default:
                    break;
            }
            slashPos = target.Center + slashPos * 50;
            Vector2 slashVel = target.Center - slashPos;
            slashVel.Normalize();
            slashVel *= 5f;
            Projectile.NewProjectile(slashPos.X, slashPos.Y, slashVel.X, slashVel.Y, mod.ProjectileType("TriswordSlash"), projectile.damage / 2, 0f, projectile.owner, 0f, 0f);
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
            projectile.ai[1]++;
            if (projectile.ai[0] != 0 || projectile.ai[1] > 240) projectile.alpha += 255 / 30;
            if (projectile.alpha >= 255) projectile.Kill();
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