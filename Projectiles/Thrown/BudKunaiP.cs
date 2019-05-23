using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class BudKunaiP : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.aiStyle = 2;
            projectile.timeLeft = 600;
            aiType = 48;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bud Kunai");
        }
        
        public override void Kill(int timeLeft)
        {
        	if (Main.rand.Next(2) == 0)
        	{
        		Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("BudKunai"));
        	}
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);

        }
    }
}