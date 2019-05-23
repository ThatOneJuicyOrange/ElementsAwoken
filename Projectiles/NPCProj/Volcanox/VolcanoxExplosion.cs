using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Volcanox
{
    public class VolcanoxExplosion : ModProjectile
    {
        public int dustCooldown = 0;
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 100;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");
        }
        public override void AI()
        {
            int maxdusts = 20;
            if (dustCooldown <= 0)
            {
                for (int i = 0; i < maxdusts; i++)
                {
                    float dustDistance = 100 + Main.rand.Next(30);
                    float dustSpeed = 10;
                    Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                    Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                    Dust vortex = Dust.NewDustPerfect(projectile.Center + offset, 6, velocity, 0, default(Color), 1.5f);
                    vortex.noGravity = true;

                    dustCooldown = 5;
                }
            }
}
    }
}