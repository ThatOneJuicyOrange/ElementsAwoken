﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AdamantiteLaser : ModProjectile
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
            projectile.timeLeft = 100;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Laser");
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
                for (int num447 = 0; num447 < 4; num447++)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Vector2 vector33 = projectile.position;
                        vector33 -= projectile.velocity * ((float)num447 * 0.25f);
                        projectile.alpha = 255;
                        int dust = Dust.NewDust(vector33, 1, 1, 60, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[dust].position = vector33;
                        Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.01f;
                        Main.dust[dust].velocity *= 0.05f;
                        Main.dust[dust].noGravity = true;
                    }
                }
                return;
            }
        }
    }
}