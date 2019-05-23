using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Permafrost
{
    public class PermafrostPortal : ModProjectile
    {
        public int laserTimer = 180;

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.scale = 1.3f;
            projectile.timeLeft = 200;
            projectile.magic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Permafrost Portal");
            Main.projFrames[projectile.type] = 4;
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
        public override void AI()
        {
            projectile.velocity.X *= 0.7f;
            projectile.velocity.X *= 0.7f;
            laserTimer--;
            if (laserTimer <= 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 33);
                int damage = 30;
                float speed = 3f;
                int type = mod.ProjectileType("PermafrostBolt");
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, speed, type, damage, 0f, 0);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, -speed, type, damage, 0f, 0);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed, 0, type, damage, 0f, 0);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -speed, 0, type, damage, 0f, 0);
                laserTimer = 180;
            }

        }
    }
}
 