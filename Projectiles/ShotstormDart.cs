using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles
{
    public class ShotstormDart : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.timeLeft = 300;
            projectile.penetrate = 1;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shotstorm Dart");
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

                if (projectile.localAI[0] % 2 == 0)
                {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 60);
                    Main.dust[dust].velocity *= 0.1f;
                    Main.dust[dust].scale *= 1.5f;
                    Main.dust[dust].noGravity = true;
                }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffType<FastPoison>(), 60);
            Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileType<ShotstormDartStuck>(), 0, 0, projectile.owner)];
            proj.ai[0] = target.whoAmI;
            proj.ai[1] = 1;
            proj.velocity = (target.Center - projectile.Center) * 0.75f;
            proj.netUpdate = true;
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