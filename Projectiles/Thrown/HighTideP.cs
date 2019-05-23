using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class HighTideP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 6;
            projectile.aiStyle = 3;
            projectile.timeLeft = 1600;
            aiType = 52;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("High Tide");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.05f) / 255f, ((255 - projectile.alpha) * 0.25f) / 255f, ((255 - projectile.alpha) * 0.01f) / 255f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] += 0.1f;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.FlaironBubble, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
    }
}