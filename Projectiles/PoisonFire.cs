using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class PoisonFire : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;

            projectile.penetrate = 3;
            projectile.timeLeft = 125;
            projectile.extraUpdates = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cremated Chaos");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.2f, 0.8f, 0.4f);
            if (projectile.timeLeft > 125)
            {
                projectile.timeLeft = 125;
            }
            if (projectile.ai[0] > 12f)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 61, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 2.75f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 2.5f;
                    int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 74, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 1f);
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }
            return;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 80);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
    }
}