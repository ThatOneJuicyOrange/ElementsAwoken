using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ThundersoulBoltMinor : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            //projectile.aiStyle = 48;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 50;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thundersoul");
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
                for (int i = 0; i < 4; i++)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Vector2 vector33 = projectile.position;
                        vector33 -= projectile.velocity * ((float)i * 0.25f);
                        projectile.alpha = 255;
                        int num448 = Dust.NewDust(vector33, 1, 1, 15, 0f, 0f, 0, default(Color), 0.75f);
                        Main.dust[num448].position = vector33;
                        Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.005f;
                        Main.dust[num448].velocity *= 0.02f;
                    }
                }
                return;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 15, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}