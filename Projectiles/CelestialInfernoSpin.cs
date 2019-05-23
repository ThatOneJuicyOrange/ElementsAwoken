using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CelestialInfernoSpin : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.timeLeft = 1000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Inferno");
        }
        public override void AI()
        {
            Vector2 offset = new Vector2(100, 0);
            int dustType = 6;
            switch ((int)projectile.ai[1])
            {
                case 0:
                    dustType = 6;
                    offset = new Vector2(100, 0);
                    break;
                case 1:
                    dustType = 197;
                    offset = new Vector2(125, 0);
                    break;
                case 2:
                    dustType = 229;
                    offset = new Vector2(150, 0);
                    break;
                case 3:
                    dustType = 242;
                    offset = new Vector2(175, 0);
                    break;
                default: break;
            }
            Player player = Main.player[projectile.owner];
            projectile.ai[0] += 0.1f;
            projectile.Center = player.Center + offset.RotatedBy(projectile.ai[0] * (Math.PI * 2 / 8));

            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}