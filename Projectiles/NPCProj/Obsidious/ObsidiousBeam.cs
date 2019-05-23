using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.hostile = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious");
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
            int type = 6;
            switch ((int)projectile.ai[1])
            {
                case 0:
                    type = 6;
                    break;
                case 1:
                    type = 75;
                    break;
                case 2:
                    type = 135;
                    break;
                case 3:
                    type = DustID.PinkFlame;
                    break;
                default: break;
            }
            if (projectile.localAI[0] > 9f)
            {
                int dustlength = 1;
                for (int i = 0; i < dustlength; i++)
                {
                    Vector2 vector33 = projectile.position;
                    vector33 -= projectile.velocity * ((float)i * (1 / dustlength));
                    projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, 1, 1, type, 0f, 0f, 0, default(Color), 0.75f);
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
            int type = 6;
            switch ((int)projectile.ai[1])
            {
                case 0:
                    type = 6;
                    break;
                case 1:
                    type = 75;
                    break;
                case 2:
                    type = 135;
                    break;
                case 3:
                    type = DustID.PinkFlame;
                    break;
                default: break;
            }
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, type, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}