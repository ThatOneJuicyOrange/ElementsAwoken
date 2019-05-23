using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CoilRound : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coilgun");
        }
        public override void AI()
        {
            int dustLength = 10;
            for (int i = 0; i < dustLength; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].position -= projectile.velocity / dustLength * (float)i;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 4; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 229, projectile.oldVelocity.X * 0.25f, projectile.oldVelocity.Y * 0.25f);
            }
        }
    }
}