using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class DictionaryP : ModProjectile
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
            DisplayName.SetDefault("Dictionary");
        }
        
        public override void Kill(int timeLeft)
        {
        	if (Main.rand.Next(2) == 0)
        	{
        		Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("Dictionary"));
        	}
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == 0)
            {
                target.AddBuff(BuffID.Confused, 200);
            }         
        }
    }
}