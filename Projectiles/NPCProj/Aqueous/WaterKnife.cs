using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Aqueous
{
    public class WaterKnife : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqueous");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.localAI[0]++;
            if (projectile.localAI[0] >= 10)
            {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 111);
                    Main.dust[dust].velocity *= 0.1f;
                    Main.dust[dust].scale *= 1f;
                    Main.dust[dust].noGravity = true;
            }
        }
    }
}