using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Explosions
{
    public class RadiantMasterDeathExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Master");
            Main.projFrames[projectile.type] = 7;

        }
        public override void AI()
        {

        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 9)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 6)
                    projectile.Kill();
            }
            return true;
        }
    }
}