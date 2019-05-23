﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DespairTip : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.aiStyle = 4;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.melee = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gladiolus");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.ai[0] == 0f)
            {
                projectile.alpha -= 50;
                if (projectile.alpha <= 0)
                {
                    projectile.alpha = 0;
                    projectile.ai[0] = 1f;
                    if (projectile.ai[1] == 0f)
                    {
                        projectile.ai[1] += 1f;
                        projectile.position += projectile.velocity * 1f;
                    }
                }
            }
            else
            {
                if (projectile.alpha < 170 && projectile.alpha + 5 >= 170)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkFlame, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f)];
                        dust.noGravity = true;
                        dust.velocity *= 0.5f;
                    }
                }
                projectile.alpha += 5;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                    return;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.PinkFlame, projectile.oldVelocity.X * 0.025f, projectile.oldVelocity.Y * 0.025f);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 4;
        }
    }
}