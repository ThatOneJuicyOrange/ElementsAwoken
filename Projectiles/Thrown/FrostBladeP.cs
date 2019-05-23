using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class FrostBladeP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 34;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.aiStyle = 3;
            projectile.timeLeft = 1200;
            aiType = 52;

            projectile.scale *= 0.9f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Blade");
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
    }
}