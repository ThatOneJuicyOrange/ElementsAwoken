using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianStickyBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.penetrate = 3;
            projectile.alpha = 255;
            projectile.magic = true;
            projectile.aiStyle = 93;
            aiType = 514;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
        }
        public override void AI()
        {
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)num121;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.HostileExplosion(projectile, 6, 100);
        }
    }
}