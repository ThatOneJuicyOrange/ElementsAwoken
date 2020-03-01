using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class CelestialBoltFriendly : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minion = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Laser");
        }
        public override void AI()
        {        
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);

            int dustType = 6;
            switch ((int)projectile.ai[1])
            {
                case 0:
                    dustType = 6;
                    break;
                case 1:
                    dustType = 197;
                    break;
                case 2:
                    dustType = 229;
                    break;
                case 3:
                    dustType = 242;
                    break;
                default: break;
            }
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