using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class FireballP : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.penetrate = 1;
            projectile.timeLeft = 220;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball");
        }
        public override void AI()
        {
            projectile.localAI[0] += 1f;
			if (projectile.localAI[0] > 4f)
			{
                float numDust = 5;
                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6)];
                    dust.velocity *= 0.1f;
                    dust.position -= projectile.velocity / numDust * (float)i;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 6, damageType: "magic");
        }
    }
}