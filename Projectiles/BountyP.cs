using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class BountyP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 200;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Signal Booster");
        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            projectile.localAI[1]++;
            if (projectile.localAI[1] < 30)
            {
                projectile.velocity *= 0.9f;
            }
            if (projectile.localAI[1] == 30)
            {
                Vector2 toTarget = new Vector2(projectile.ai[0] - projectile.Center.X, projectile.ai[1] - projectile.Center.Y);
                toTarget.Normalize();
                projectile.velocity = toTarget * 15f;
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, DustID.PinkFlame, damageType: "melee");
        }
    }
}