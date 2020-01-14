using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class Sandball : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.friendly = true;
            projectile.thrown = true;

            projectile.penetrate = 1;
            projectile.aiStyle = 2;
            projectile.timeLeft = 600;
            aiType = 48;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandball");
        }
        public override void Kill(int timeLeft)
        {
            for (int l = 0; l < 3; l++)
            { 
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 2, projectile.height + 2, 32, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}