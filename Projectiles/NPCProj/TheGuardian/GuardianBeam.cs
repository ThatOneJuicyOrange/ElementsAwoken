using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
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
            for (int i = 0; i < 2; i++)
            {
                Vector2 vector33 = projectile.position;
                vector33 -= projectile.velocity * ((float)i * 0.25f);
                projectile.alpha = 255;
                int dust = Dust.NewDust(vector33, 1, 1, 6, 0f, 0f, 0, default(Color), 0.75f);
                Main.dust[dust].position = vector33;
                Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                Main.dust[dust].velocity *= 0.05f;
                Main.dust[dust].noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}