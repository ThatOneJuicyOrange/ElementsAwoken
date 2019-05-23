using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class EnergyWeaverBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Beam");
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
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)
            {
                int dustlength = 4;
                for (int i = 0; i < dustlength; i++)
                {
                    int dust = Main.rand.Next(2) == 0 ? 135 : 164;

                    Vector2 vector33 = projectile.position;
                    vector33 -= projectile.velocity * ((float)i * (1 / dustlength));
                    projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, 1, 1, dust, 0f, 0f, 0, default(Color), 0.75f);
                    Main.dust[num448].position = vector33;
                    Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[num448].velocity *= 0.05f;
                    Main.dust[num448].noGravity = true;
                }
                return;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Main.rand.Next(2) == 0 ? 135 : 164;

                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, dust, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}