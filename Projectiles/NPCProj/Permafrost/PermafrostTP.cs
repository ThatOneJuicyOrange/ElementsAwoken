using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Permafrost
{
    public class PermafrostTP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 140;
            projectile.height = 160;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Permafrost");
            Main.projFrames[projectile.type] = 6;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            projectile.ai[0]++;
            if (projectile.ai[1] != 0)
            {
                projectile.alpha += 255 / 20;
                if (projectile.alpha >= 255) projectile.Kill();
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (projectile.ai[1] == 0)
            {
                projectile.frameCounter++;
                if (projectile.frameCounter >= 2)
                {
                    projectile.frame++;
                    projectile.frameCounter = 0;
                    if (projectile.frame == 5)
                        projectile.ai[1]++;
                }
            }
            return true;
        }
    }
}