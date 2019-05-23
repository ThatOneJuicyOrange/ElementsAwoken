using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class ThrowableBookP : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.aiStyle = 2;
            projectile.timeLeft = 600;
            aiType = 48;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwable Book");
        }
        
        public override void Kill(int timeLeft)
        {
        	if (Main.rand.Next(2) == 0)
        	{
        		Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("ThrowableBook"));
        	}
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);

        }
    }
}