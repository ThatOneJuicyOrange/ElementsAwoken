using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.InfinityGauntlet
{
    public class FirePortal : ModProjectile
    {
        public int shootTimer = 0;
        public override void SetDefaults()
        {
            projectile.scale = 1.0f;
            projectile.width = 32;
            projectile.height = 32;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Portal");
        }
        public override void AI()
        {
            shootTimer--;
            if (shootTimer <= 0)
            {
                float randAi0 = Main.rand.Next(10, 80) * 0.001f;
                if (Main.rand.Next(2) == 0)
                {
                    randAi0 *= -1f;
                }
                float randAi1 = Main.rand.Next(10, 80) * 0.001f;
                if (Main.rand.Next(2) == 0)
                {
                    randAi1 *= -1f;
                }
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-10, 10) * .25f, Main.rand.Next(-10, 10) * .25f, mod.ProjectileType("InfinityFireTentacle"), 100, 0, projectile.owner, randAi0, randAi1);
                shootTimer = 10;
            }
        }
    }
}