using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients.Kirvein
{
    public class KirveinExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 40;
            projectile.scale *= 1.2f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kirvein");
            Main.projFrames[projectile.type] = 7;

        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("AncientGreen"));
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.5f;
            Main.dust[dust].velocity *= 1f;
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