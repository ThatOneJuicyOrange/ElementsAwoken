using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class MagmaroxP : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 220;
            projectile.melee = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magmarox");
        }
        public override void AI()
        {
            projectile.localAI[0] += 1f;
			if (projectile.localAI[0] > 4f)
			{
				for (int num468 = 0; num468 < 10; num468++)
				{
                    if (Main.rand.Next(4) == 0)
                    {
                        int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[dust1].noGravity = true;
                        Main.dust[dust1].velocity *= 0f;
                        int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[dust2].noGravity = true;
                        Main.dust[dust2].velocity *= 0f;
                    }
                }
			}
        }

        public override void Kill(int timeLeft)
        {
            ProjectileUtils.Explosion(projectile, 6, damageType: "melee");
        }
    }
}