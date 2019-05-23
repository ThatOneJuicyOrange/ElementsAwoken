using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class ZergFireball : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 200;
            projectile.magic = true;
            projectile.scale *= 1.2f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zerg Caster");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            Lighting.AddLight(projectile.Center, 2f, 0.5f, 0.5f);
            int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 127, projectile.velocity.X * 0.15f, projectile.velocity.Y * 0.15f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale *= 1.5f;
            projectile.localAI[0]++;
            if (projectile.localAI[0] < 180)
            {
                projectile.velocity.X *= 1.01f;
                projectile.velocity.Y *= 1.01f;
            }
        }
    }
}