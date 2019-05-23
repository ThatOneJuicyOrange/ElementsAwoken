using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class WatershotP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Watershot");
        }
        public override void AI()
        {
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 15;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 59)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / 6f * (float)num121;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.Bubble, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.Bubble, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                }
            }
            projectile.Kill();
            return false;
        }
    }   
}