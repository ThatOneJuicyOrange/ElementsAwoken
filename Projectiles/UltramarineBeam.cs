using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class UltramarineBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 320;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultramarine");
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
            projectile.localAI[1]++;
            if (projectile.ai[0] == 1)
            {
                projectile.penetrate = 1;
                projectile.localAI[1] = 10;
                if (projectile.localAI[0] == 0)
                {
                    projectile.timeLeft = 60;
                    projectile.localAI[0]++;
                }
            }
            if (projectile.localAI[1] >= 10)
            {
                int dustlength = 2;
                for (int i = 0; i < dustlength; i++)
                {
                    Vector2 vector33 = projectile.position;
                    vector33 -= projectile.velocity * ((float)i * (1 / dustlength));
                    projectile.alpha = 255;
                    int dust = Dust.NewDust(vector33, 1, 1, 135, 0f, 0f, 0, default(Color), 0.75f);
                    Main.dust[dust].position = vector33;
                    Main.dust[dust].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[dust].velocity *= 0.05f;
                    Main.dust[dust].noGravity = true;
                }
                if (Main.rand.Next(10) == 0 && projectile.ai[0] == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 8, mod.ProjectileType("UltramarineBeam"), projectile.damage, projectile.knockBack, projectile.owner, 1);
                }
            }
            return;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 135, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}