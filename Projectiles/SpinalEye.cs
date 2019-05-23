using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class SpinalEye : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;

            projectile.aiStyle = 1;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;

            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giga Eye");
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.05f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Bleeding"), 200);
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 5, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}