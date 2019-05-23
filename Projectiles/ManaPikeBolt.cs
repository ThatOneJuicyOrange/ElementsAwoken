using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ManaPikeBolt : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 25;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Manashard Pike");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int i = 0; i < 3; i++)
            {
                Vector2 pos = new Vector2(projectile.position.X, projectile.position.Y);
                int num348 = Dust.NewDust(pos, projectile.width, projectile.height, 234, projectile.velocity.X, projectile.velocity.Y, 50, default(Color), 1.2f);
                Main.dust[num348].position = (Main.dust[num348].position + projectile.Center) / 2f;
                Main.dust[num348].noGravity = true;
                Main.dust[num348].velocity *= 0.5f;
            }
        }
        public override void Kill(int timeLeft)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(10);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ManaPikeBolt2"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
    }
}