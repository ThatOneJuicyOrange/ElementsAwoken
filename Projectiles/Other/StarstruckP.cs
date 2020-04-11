using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Other
{
    public class StarstruckP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 60000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hands of Despair");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = (int)projectile.ai[0];
            return true;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.8f, 0.2f, 0.6f);
            projectile.velocity *= 0.995f;
            projectile.rotation += projectile.velocity.X * 0.2f;
            projectile.ai[1]++;
            if (projectile.ai[1] > 20)
            {
                projectile.alpha += 255 / 120;
                if (projectile.alpha >= 255) projectile.Kill();
            }
        }
    }
}