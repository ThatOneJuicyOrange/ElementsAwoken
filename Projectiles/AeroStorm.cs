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
        public int shootTimer = 5;
        public int shootTimer2 = 5;
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
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
            shootTimer--;
            shootTimer2--;
            if (shootTimer <= 0)
            {
                int rand = Main.rand.Next(-18, 18);
                Projectile.NewProjectile(projectile.Center.X + rand, projectile.Center.Y + 10, 0, 10, ProjectileID.RainFriendly, projectile.damage, 0, projectile.owner);
                shootTimer = 15;
            }
            if (shootTimer2 <= 0)
            {
                int rand = Main.rand.Next(-18, 18);
                int rand2 = Main.rand.Next(-2, 2);
                Projectile.NewProjectile(projectile.Center.X + rand, projectile.Center.Y, rand2, 10, mod.ProjectileType("AeroLightning"), projectile.damage * 2, 0, projectile.owner);
                shootTimer2 = 60 + Main.rand.Next(0, 90);
            }
            if (projectile.timeLeft <= 100)
            {
                projectile.alpha += 3;
            }
        }
    }
}