using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class WokeBeam : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            //projectile.aiStyle = 48;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 320;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("WOKE");
        }
        public override void AI()
        {
            if (projectile.velocity.X != projectile.velocity.X)
            {
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -projectile.velocity.X;
            }
            if (projectile.velocity.Y != projectile.velocity.Y)
            {
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -projectile.velocity.Y;
            }

            int dustlength = 4;
            for (int i = 0; i < dustlength; i++)
            {
                Vector2 vector33 = projectile.position;
                vector33 -= projectile.velocity * ((float)i * (1 / dustlength));
                projectile.alpha = 255;
                int num448 = Dust.NewDust(vector33, 1, 1, 219, 0f, 0f, 0, default(Color), 0.75f);
                Main.dust[num448].position = vector33;
                Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                Main.dust[num448].velocity *= 0.05f;
                Main.dust[num448].noGravity = true;
            }
            return;
        }
    }
}