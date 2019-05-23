using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheCelestial
{
    public class CelestialStarShot : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 1200;
            projectile.light = 2f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Celestials");
        }
        public override void AI()
        {
            projectile.rotation += 0.1f;
            Lighting.AddLight(projectile.Center, 0.0f, 0.2f, 0.4f);
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.Center, 4, 4, 15)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}