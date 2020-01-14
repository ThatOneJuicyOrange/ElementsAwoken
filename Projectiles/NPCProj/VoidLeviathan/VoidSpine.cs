using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.VoidLeviathan
{
    public class VoidSpine : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.penetrate = -1;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 600;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Spine");
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.alpha != 0) return false;
            return base.CanHitPlayer(target);
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.2f, 0.55f);

            projectile.ai[0]++;
            if (projectile.ai[1] == 0)
            {
                projectile.scale = 0.05f;
                projectile.ai[1]++;
            }
            if (projectile.ai[0] < 15)
            {
                if (projectile.scale < 0.5f) projectile.scale += 0.5f / 15f;
                else projectile.scale = 0.5f;
            }
            if (projectile.ai[0] > 30)
            {
                projectile.alpha += 255 / 30;
                if (projectile.alpha >= 255)projectile.Kill();
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (!ModContent.GetInstance<Config>().lowDust && Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ExtinctionCurse"), 80, true);
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