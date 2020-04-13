using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CombustiaWave : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 100;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
            projectile.knockBack = 10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Combustia");
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
            target.AddBuff(BuffID.OnFire, 200);
        }

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 6, damageType: "melee");
        }
    }
}