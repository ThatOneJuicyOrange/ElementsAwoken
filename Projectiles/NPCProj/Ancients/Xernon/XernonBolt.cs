using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients.Xernon
{
    public class XernonBolt : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.alpha = 255;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Xernon");
        }
        public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                int num464 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 113, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num464].noGravity = true;
                Dust dust = Main.dust[num464];
                dust.velocity *= 0.5f;
                dust = Main.dust[num464];
                dust.velocity += projectile.velocity * 0.1f;
            }

        }
    }
}