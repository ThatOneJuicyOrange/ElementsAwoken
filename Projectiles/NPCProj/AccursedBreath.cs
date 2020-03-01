using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class AccursedBreath : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Accursed Fire");
            Main.projFrames[projectile.type] = 7;

        }
        public override void AI()
        {
            projectile.velocity *= 0.99f;
            if (projectile.ai[0] == 0)
            {
                projectile.scale = 0.3f;
                projectile.ai[0]++;
            }
            if (projectile.scale < 1) projectile.scale += 1f / 20f;
            else projectile.scale = 1;
            if (projectile.ai[1] == 1)
            {
                projectile.alpha += 255 / 10;
                if (projectile.alpha >= 255) projectile.Kill();
            }
            if (Main.rand.NextBool(5))
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].velocity *= 1f;
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter % 9 == 0)
            {
                projectile.frame++;
                if (projectile.frame > 6)
                {
                    projectile.frame = 6;
                    projectile.ai[1] = 1;
                }
            }
            return true;
        }
    }
}