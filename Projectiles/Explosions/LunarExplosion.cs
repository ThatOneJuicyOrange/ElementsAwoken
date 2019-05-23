using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Explosions
{
    public class LunarExplosion : ModProjectile
    {
        public bool playedSound = false;
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 40;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");
            Main.projFrames[projectile.type] = 5;

        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.5f;
            Main.dust[dust].velocity *= 1f;
            if (!playedSound)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
                playedSound = true;
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 4)
                    projectile.Kill();
            }
            return true;
        }
    }
}