using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class LightningExplosion : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning");
        }
        public override void AI()
        {
            for (int num134 = 0; num134 < 10; num134++)
            {
                float x = projectile.position.X - projectile.velocity.X / 10f * (float)num134;
                float y = projectile.position.Y - projectile.velocity.Y / 10f * (float)num134;
                int num135 = Dust.NewDust(new Vector2(x, y), 1, 1, 15, 0f, 0f, 0, default(Color), 1.25f);
                Main.dust[num135].alpha = projectile.alpha;
                Main.dust[num135].position.X = x;
                Main.dust[num135].position.Y = y;
                Main.dust[num135].velocity *= 0f;
                Main.dust[num135].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 15, 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                projectile.ai[0] += 0.1f;
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }
    }
}