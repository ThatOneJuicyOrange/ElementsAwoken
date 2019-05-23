using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class WyvernBreath1 : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wyvern Breath");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 3; num121++)
            {
                {
                    Vector2 pos = new Vector2(projectile.position.X, projectile.position.Y);
                    int num348 = Dust.NewDust(pos, projectile.width, projectile.height, 234, projectile.velocity.X, projectile.velocity.Y, 50, default(Color), 1.2f);
                    Main.dust[num348].position = (Main.dust[num348].position + projectile.Center) / 2f;
                    Main.dust[num348].noGravity = true;
                    Main.dust[num348].velocity *= 0.5f;
                }
            }
            projectile.velocity.Y += 0.05f;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
        public override void Kill(int timeLeft)
        {
            int numberProjectiles = 4;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-5, 5), (float)Main.rand.Next(-5, 5));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value15.X, value15.Y, mod.ProjectileType("WyvernBreath2"), 32, 2f, projectile.owner, 0f, 0f);
            }
        }
    }
}