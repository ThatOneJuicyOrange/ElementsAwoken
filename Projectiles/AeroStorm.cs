using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles
{
    public class AeroStorm : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 28;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 600;
            projectile.light = 2f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aero Storm");
            Main.projFrames[projectile.type] = 6;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 5)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.ai[0]--;
            projectile.ai[1]--;
            int spawnWidth = projectile.width - 4;
            if (projectile.ai[0] <= 0)
            {
                Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-spawnWidth / 2, spawnWidth / 2), projectile.position.Y + projectile.height, 0, 10, ProjectileID.RainFriendly, projectile.damage, 0, projectile.owner);
                projectile.ai[0] = 15;
            }
            if (projectile.ai[1] <= 0)
            {
                Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-spawnWidth / 2, spawnWidth / 2), projectile.position.Y + projectile.height, Main.rand.NextFloat(-2, 2), 10, mod.ProjectileType("AeroLightning"), projectile.damage * 2, 0, projectile.owner);
                projectile.ai[1] = Main.rand.Next(60, 120);
                projectile.netUpdate = true;
            }
            if (projectile.timeLeft <= 100)
            {
                projectile.alpha += 255 / 60;
                if (projectile.alpha >= 255) projectile.Kill();
            }
        }
    }
}