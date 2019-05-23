using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class CrowStorm : ModProjectile
    {
        public int shootTimer = 5;
        public int shootTimer2 = 5;
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
            projectile.light = 0.4f;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aero Storm");
            Main.projFrames[projectile.type] = 6;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 5)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            if (projectile.localAI[0] < 10)
            {
                projectile.alpha -= 25;
            }
            if (projectile.localAI[0] > 60)
            {
                projectile.alpha += 10;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                }
            }
        }
    }
}