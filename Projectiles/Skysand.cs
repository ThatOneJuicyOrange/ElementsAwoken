using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    class Skysand : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.aiStyle = 45;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.tileCollide = true;
            projectile.timeLeft = 300;
            projectile.alpha = 255;
            projectile.scale = 1.1f;
            projectile.magic = true;
            projectile.extraUpdates = 1;
            aiType = 239;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skysand");
        }
        public override void AI()
        {
        	for (int num121 = 0; num121 < 5; num121++)
			{
				Dust dust4 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 138, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f)];
				dust4.velocity = Vector2.Zero;
				dust4.position -= projectile.velocity / 5f * (float)num121;
				dust4.noGravity = true;
				dust4.scale = 0.8f;
			}
        }
    }
}