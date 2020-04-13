using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class SignalBoost : ModProjectile
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

            projectile.scale *= 0.7f;
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

            projectile.scale = projectile.localAI[1] * 0.03f;

            if (projectile.scale <= 0.7f)
            {
                projectile.scale = 0.7f;
            }
            if (projectile.scale >= 2f)
            {
                projectile.scale = 2f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage && !target.boss)
            {
                Vector2 knockBack = (target.Center - projectile.Center);
                knockBack.Normalize();
                float kbMulti = projectile.localAI[1] * 0.12f;
                if (kbMulti <= 1.5f)
                {
                    kbMulti = 1.5f;
                }
                if (kbMulti >= 6.5f)
                {
                    kbMulti = 6.5f;
                }
               // Main.NewText("" + kbMulti, Color.White);
                target.velocity = (target.velocity + knockBack) * kbMulti;
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, DustID.PinkFlame, damageType: "magic");
        }
    }
}