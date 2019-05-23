using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Azana
{
    public class AzanaBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.scale = 1.0f;
            projectile.width = 32;
            projectile.height = 32;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azana");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.localAI[0] == 0f)
            {
                projectile.alpha = 0;
                projectile.scale = 1.1f;
                int num1 = 0;
                while ((float)num1 < 16f)
                {
                    Vector2 vector2 = Vector2.UnitX * 0f;
                    vector2 += -Vector2.UnitY.RotatedBy((double)((float)num1 * (6.28318548f / 16f)), default(Vector2)) * new Vector2(1f, 4f);
                    vector2 = vector2.RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int dust1 = Dust.NewDust(projectile.Center, 0, 0, 127, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[dust1].scale = 1.5f;
                    Main.dust[dust1].noGravity = true;
                    Main.dust[dust1].position = projectile.Center + vector2;
                    Main.dust[dust1].velocity = projectile.velocity * 0f + vector2.SafeNormalize(Vector2.UnitY) * 1f;
                    num1++;
                }
                projectile.localAI[0] = 1f;
            }
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ChaosBurn"), 180);
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