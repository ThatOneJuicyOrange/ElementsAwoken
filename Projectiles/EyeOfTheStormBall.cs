using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class EyeOfTheStormBall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 220;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye of the Storm");
        }
        public override void AI()
        {
            projectile.localAI[0] += 1f;
			if (projectile.localAI[0] > 4f)
			{
				for (int num468 = 0; num468 < 10; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 15, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
			}
            if (projectile.localAI[1] > 4f)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 9);
                projectile.localAI[0] += 1f;
            }

        }

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 15, damageType: "melee");
        }
    }
}