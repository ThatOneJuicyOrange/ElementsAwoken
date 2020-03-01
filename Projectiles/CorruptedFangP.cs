using ElementsAwoken.Buffs;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CorruptedFangP : ModProjectile
    {
        private bool[] hasHit = new bool[Main.maxNPCs];
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;

            projectile.friendly = true;
            projectile.penetrate = 5;
            projectile.timeLeft = 300;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupted Fang");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.player[projectile.owner].AddBuff(ModContent.BuffType<VilePower>(), 300, false);

            hasHit[target.whoAmI] = true;
            bool moreEnemies = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.whoAmI != target.whoAmI && Vector2.Distance(nPC.Center, projectile.Center) < 600 && nPC.active && nPC.CanBeChasedBy(projectile, false) && Collision.CanHitLine(projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    moreEnemies = true;
                }
            }
            if (!moreEnemies) projectile.Kill();
        }
        public override bool ShouldUpdatePosition()
        {
            if (projectile.ai[1] < 45) return false;
            return base.ShouldUpdatePosition();
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.ai[1]++;

            if (projectile.ai[1] >= 45)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, Main.rand.NextBool(6) ? 75 : 46);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.8f;
                Main.dust[dust].noGravity = true;
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
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 46, 0f, 0f, 100, default);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}