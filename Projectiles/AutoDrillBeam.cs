﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AutoDrillBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            //projectile.aiStyle = 48;

            projectile.friendly = true;
            projectile.ranged = true;

            projectile.penetrate = -1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 9000;
            projectile.tileCollide = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dubstep Beam");
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
            int dustLength = 4;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 64)];
                dust.velocity = Vector2.Zero;
                dust.position -= projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
            }
        }
    }
}