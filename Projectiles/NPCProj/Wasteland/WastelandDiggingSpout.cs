using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Wasteland
{
    public class WastelandDiggingSpout : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 400;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wasteland");
        }
        public override void AI()
        {
            projectile.velocity *= 0f;
            for (int k = 0; k < 3; k++)
            {
                int dust2 = Dust.NewDust(projectile.position, projectile.width, 32, 32, 0f, -16f, 100, default(Color), 1.5f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 1f;
            }
        }

        public override void Kill(int timeLeft)
        {

        }
    }
}