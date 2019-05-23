using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ManaRound : ModProjectile
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
            DisplayName.SetDefault("Mana Round");
        }
        public override void AI()
        {
            for (int num134 = 0; num134 < 6; num134++)
            {
                float x = projectile.position.X - projectile.velocity.X / 10f * (float)num134;
                float y = projectile.position.Y - projectile.velocity.Y / 10f * (float)num134;
                int num135 = Dust.NewDust(new Vector2(x, y), 1, 1, 234, 0f, 0f, 0, default(Color), 1.25f);
                Main.dust[num135].alpha = projectile.alpha;
                Main.dust[num135].position.X = x;
                Main.dust[num135].position.Y = y;
                Main.dust[num135].velocity *= 0f;
                Main.dust[num135].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 234, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f); //206 160 226
            }
        }
    }
}