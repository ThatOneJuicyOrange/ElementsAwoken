using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AtaxiaBlade : ModProjectile
    {
        private float[] prevRot = new float[10];
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;

            projectile.friendly = true;
            projectile.melee = true;

            projectile.timeLeft = 420;
            projectile.penetrate = -1;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ataxia");
        }
        public override void AI()
        {
            projectile.ai[0]++;
            if (projectile.ai[0] % 2 == 0)
            {
                for (int i = projectile.oldPos.Length - 1; i > 0; i--)
                {
                    prevRot[i] = prevRot[i - 1];
                }
                prevRot[0] = projectile.rotation;
            }
            projectile.rotation += 0.25f;
            projectile.velocity *= 0.98f;
            if (Main.rand.Next(15) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("AncientRed"));
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 1.1f;
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
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, prevRot[k], drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType("AncientRed"), 0f, 0f, 100, default);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}