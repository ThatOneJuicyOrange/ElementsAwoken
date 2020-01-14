using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.GemLasers
{
    public class GemLaser : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 4;
            projectile.alpha = 255;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gem Laser");
        }
        public override void AI()
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, GetDustID())];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
            }
        }
        private int GetDustID()
        {
            switch (projectile.ai[0])
            {
                case 0:
                    return 62;
                case 1:
                    return 64;
                case 2:
                    return 59;
                case 3:
                    return 61;
                case 4:
                    return 64;
                case 5:
                    return 60;
                case 6:
                    return 63;
                default:
                    return 62;
            }
        }
    }
}