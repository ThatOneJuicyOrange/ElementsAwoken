using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class IceReaverP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 3;
            projectile.alpha = 255;
            projectile.magic = true;
            projectile.aiStyle = 93;
            //aiType = 514;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Reaver");
        }
        public override void AI()
        {
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 135)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)num121;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 135, damageType: "melee");
        }
    }
}