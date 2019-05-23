using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Amulet
{
    public class AmuletProj : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.damage = 100;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
            
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amulet of Destruction");
        }
        public override void AI()
        {
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 12);
            }
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame)];
                dust.velocity *= 0.1f;
                dust.position -= projectile.velocity / 5f * (float)num121;
                dust.noGravity = true;
                dust.scale = 1f;

            }
        }
    }
}