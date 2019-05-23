using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheCelestial
{
    public class CelestialNebulaPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 32;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.scale = 1.3f;
            projectile.timeLeft = 240;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Celestial");
            Main.projFrames[projectile.type] = 4;
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.99f;
            projectile.velocity.X *= 0.99f;
            Lighting.AddLight(projectile.Center, 1f, 1f, 1f);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 33);
            int type = mod.ProjectileType("TheCelestialLaser");
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 8, type, projectile.damage, 0f, 0, 3);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, -8, type, projectile.damage, 0f, 0, 3);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 8, 0, type, projectile.damage, 0f, 0, 3);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -8, 0, type, projectile.damage, 0f, 0, 3);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}