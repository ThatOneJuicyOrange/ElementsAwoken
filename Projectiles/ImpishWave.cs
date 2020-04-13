using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ImpishWave : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;

            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 200;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impish Wave");
        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 60);
            target.AddBuff(mod.BuffType("ImpishCurse"), 300);
            for (int l = 0; l < 5; l++)
            {
                int dir = l == 1 ? -1 : 1;
                Projectile.NewProjectile(target.Center.X, target.Center.Y - 200 * dir, 0f, 8 * dir, mod.ProjectileType("ImpishFireball"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 6, damageType: "magic");
        }
    }
}