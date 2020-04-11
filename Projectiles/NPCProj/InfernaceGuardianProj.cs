using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class InfernaceGuardianProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 86;
            projectile.height = 86;

            projectile.hostile = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
            projectile.alpha = 255;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Spell");
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.alpha > 150) return false;
            return base.CanHitPlayer(target);
        }
        public override void AI()
        {
            projectile.rotation += 0.05f;
            if (projectile.ai[0] == 0)
            {
                projectile.scale = 0.001f;
                projectile.ai[0]++;
            }
            if (projectile.alpha > 100) projectile.alpha -= 255 / 90;
            else projectile.alpha = 100;
            if (projectile.scale < 1) projectile.scale += 1f / 60f;
            else projectile.scale = 1;

            int dust = Dust.NewDust(projectile.Center - new Vector2(projectile.width / 2, projectile.height / 2) * projectile.scale, (int)(projectile.width * projectile.scale), (int)(projectile.height * projectile.scale), DustID.Fire);
            Main.dust[dust].velocity = -projectile.velocity;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
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
            ProjectileUtils.HostileExplosion(projectile, new int[] { 6 }, projectile.damage);
        }
    }
}