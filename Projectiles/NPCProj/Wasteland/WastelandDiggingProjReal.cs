using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Wasteland
{
    public class WastelandDiggingProjReal : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 220;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wasteland");
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.2f;
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("WastelandDiggingSpoutReal"), 0, 0, 0, projectile.ai[0]);

        }
    }
}