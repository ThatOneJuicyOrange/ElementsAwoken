using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ManaRound : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mana Round");
        }
        public override void AI()
        {
            projectile.localAI[0]++;
            if (projectile.localAI[0] > 4)
            {
                float numDust = 4;
                for (int l = 0; l < numDust; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 234)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= projectile.velocity / numDust * (float)l;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 4; k++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 234, projectile.oldVelocity.X * 0.15f, projectile.oldVelocity.Y * 0.15f)]; ;
                dust.noLight = true;
            }
        }
    }
}