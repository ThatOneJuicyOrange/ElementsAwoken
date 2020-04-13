using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ShimmersparkStrike : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;

            projectile.penetrate = -1;

            projectile.ranged = true;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shimmerspark");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, GetDustID());
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            if (Main.rand.Next(2) == 0)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X * 0.05f, projectile.velocity.Y * 0.05f).RotatedByRandom(MathHelper.ToRadians(7));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ShimmerShrapnel"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
        private int GetDustID()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return mod.DustType("AncientRed");
                case 1:
                    return mod.DustType("AncientGreen");
                case 2:
                    return mod.DustType("AncientBlue");
                case 3:
                    return mod.DustType("AncientPink");
                default:
                    return mod.DustType("AncientRed");
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
            ProjectileUtils.Explosion(projectile, new int[] {mod.DustType("AncientRed"), mod.DustType("AncientGreen"), mod.DustType("AncientBlue"), mod.DustType("AncientPink") }, damageType: "ranged");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            ProjectileUtils.Explosion(projectile, new int[] { mod.DustType("AncientRed"), mod.DustType("AncientGreen"), mod.DustType("AncientBlue"), mod.DustType("AncientPink") }, damageType: "ranged");
        }
    }
}