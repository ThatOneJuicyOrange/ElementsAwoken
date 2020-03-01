using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
	public class NightbreakerBall : ModProjectile
	{
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.aiStyle = 0;
            projectile.alpha = 255;
			projectile.timeLeft = 150;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.melee = true;          
			projectile.penetrate = 2;
		}
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightbreaker");
        }
        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 14)];
                dust.velocity *= 0.4f;
                dust.position -= projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
                dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 27)];
                dust.velocity *= 0.4f;
                dust.position -= projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}